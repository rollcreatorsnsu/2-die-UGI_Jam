using System;
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
        lastHitPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            game.farPlayer.Attack(lastHitPoint);
            game.nearPlayer.Attack();
        }

        if (Input.GetKey(KeyCode.W))
        {
            circleCenter += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            circleCenter += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            circleCenter += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            circleCenter += Vector3.right * speed * Time.deltaTime;
        }

        circleRadius += speed * Input.mouseScrollDelta.y;

        Vector3 newFarPlayerPosition = game.farPlayer.transform.position;
        newFarPlayerPosition += (lastHitPoint - newFarPlayerPosition).normalized * speed;
        newFarPlayerPosition += (circleCenter - newFarPlayerPosition).normalized *
                                (Vector3.Distance(newFarPlayerPosition, circleCenter) - circleRadius);
        game.farPlayer.transform.position = newFarPlayerPosition;
        game.nearPlayer.transform.position = newFarPlayerPosition + (circleCenter - newFarPlayerPosition) * 2;

        float signedAngle = Vector2.SignedAngle(Vector2.up, lastHitPoint - game.farPlayer.transform.position);
        game.farPlayer.transform.rotation = Quaternion.Euler(0, 0, signedAngle);
        signedAngle = Vector2.SignedAngle(Vector2.up, lastHitPoint - game.nearPlayer.transform.position);
        game.nearPlayer.transform.rotation = Quaternion.Euler(0, 0, signedAngle);
    }
}
