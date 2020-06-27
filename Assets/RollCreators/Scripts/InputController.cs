using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    public float speed = 11;
    [SerializeField] private Game game;
    [SerializeField] private Vector3 circleCenter;
    [SerializeField] private float circleRadius;
    private Vector3 lastHitPoint;
    
    void Update()
    {
        if (game.health <= 0 || game.isPaused) return;
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
            circleCenter += Vector3.up * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            circleCenter += Vector3.left * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            circleCenter += Vector3.down * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            circleCenter += Vector3.right * speed;
        }

        circleRadius += speed * Input.mouseScrollDelta.y;

        Vector3 newFarPlayerPosition = game.farPlayer.transform.position;
        newFarPlayerPosition += (newFarPlayerPosition - lastHitPoint).normalized * speed;
        newFarPlayerPosition += (newFarPlayerPosition - circleCenter).normalized *
                                (Vector3.Distance(newFarPlayerPosition, circleCenter) - circleRadius);
        game.farPlayer.transform.position = newFarPlayerPosition;
        game.nearPlayer.transform.position = newFarPlayerPosition + (newFarPlayerPosition - circleCenter) * 2;

        game.farPlayer.transform.LookAt(lastHitPoint);
        game.nearPlayer.transform.LookAt(lastHitPoint);
    }
}
