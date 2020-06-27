using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private Vector3 circleCenter;
    [SerializeField] private float circleRadius;
    [SerializeField] private float coordPerFrameWalking;
    [SerializeField] private float coordPerScrollRadius;
    [SerializeField] private float coordPerFrameToCursorWalking;
    private Vector3 lastHitPoint;
    
    void Update()
    {
        if (game.health <= 0) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lastHitPoint = hit.point;
        }

        if (Input.GetMouseButtonUp(0))
        {
            game.farPlayer.Attack(lastHitPoint);
            game.nearPlayer.Attack();
        }

        if (Input.GetKey(KeyCode.W))
        {
            circleCenter += Vector3.up * coordPerFrameWalking;
        }

        if (Input.GetKey(KeyCode.A))
        {
            circleCenter += Vector3.left * coordPerFrameWalking;
        }

        if (Input.GetKey(KeyCode.S))
        {
            circleCenter += Vector3.down * coordPerFrameWalking;
        }

        if (Input.GetKey(KeyCode.D))
        {
            circleCenter += Vector3.right * coordPerFrameWalking;
        }

        circleRadius += coordPerScrollRadius * Input.mouseScrollDelta.y;

        Vector3 newFarPlayerPosition = game.farPlayer.transform.position;
        newFarPlayerPosition += (newFarPlayerPosition - lastHitPoint).normalized * coordPerFrameToCursorWalking;
        newFarPlayerPosition += (newFarPlayerPosition - circleCenter).normalized *
                                (Vector3.Distance(newFarPlayerPosition, circleCenter) - circleRadius);
        game.farPlayer.transform.position = newFarPlayerPosition;
        game.nearPlayer.transform.position = newFarPlayerPosition + (newFarPlayerPosition - circleCenter) * 2;

        game.farPlayer.transform.LookAt(lastHitPoint);
        game.nearPlayer.transform.LookAt(lastHitPoint);
    }
}
