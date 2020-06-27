using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private FarPlayer farPlayer;
    [SerializeField] private NearPlayer nearPlayer;
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
            farPlayer.Attack(lastHitPoint);
            nearPlayer.Attack();
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

        Vector3 newFarPlayerPosition = farPlayer.transform.position;
        newFarPlayerPosition += (newFarPlayerPosition - lastHitPoint).normalized * coordPerFrameToCursorWalking;
        newFarPlayerPosition += (newFarPlayerPosition - circleCenter).normalized *
                                (Vector3.Distance(newFarPlayerPosition, circleCenter) - circleRadius);
        farPlayer.transform.position = newFarPlayerPosition;
        nearPlayer.transform.position = newFarPlayerPosition + (newFarPlayerPosition - circleCenter) * 2;

        farPlayer.transform.LookAt(lastHitPoint);
        nearPlayer.transform.LookAt(lastHitPoint);
    }
}
