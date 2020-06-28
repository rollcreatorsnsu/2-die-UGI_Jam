using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        lastHitPoint.z = 0;

        Vector3 cameraPrevPos = Camera.main.transform.position;
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos = circleCenter;
        cameraPos.z = -10;
        Camera.main.transform.position = cameraPos;
        if (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x < -game.GetHorizontalSize() ||
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x > game.GetHorizontalSize())
        {
            cameraPos.x = cameraPrevPos.x;
        }
        if (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y < -game.GetVerticalSize() ||
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y > game.GetVerticalSize())
        {
            cameraPos.y = cameraPrevPos.y;
        }
        Camera.main.transform.position = cameraPos;
        if (Input.GetMouseButton(0))
        {
            game.farPlayer.Attack();
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

        circleRadius += speed * Input.mouseScrollDelta.y * Time.deltaTime * 10;
        if (circleRadius < 0) circleRadius = 0;

        Vector3 newFarPlayerPosition = game.farPlayer.transform.position;
        newFarPlayerPosition += (lastHitPoint - newFarPlayerPosition).normalized * speed * Time.deltaTime;

        float diff = Vector3.Distance(newFarPlayerPosition, circleCenter) - circleRadius;
        newFarPlayerPosition += (circleCenter - newFarPlayerPosition).normalized * diff;
        game.farPlayer.transform.position = newFarPlayerPosition;
        game.nearPlayer.transform.position = newFarPlayerPosition + (circleCenter - newFarPlayerPosition) * 2;

        float signedAngle = Vector2.SignedAngle(Vector2.up, lastHitPoint - game.farPlayer.transform.position);
        game.farPlayer.transform.rotation = Quaternion.Euler(0, 0, signedAngle);
        signedAngle = Vector2.SignedAngle(Vector2.up, game.nearPlayer.transform.position - game.farPlayer.transform.position);
        game.nearPlayer.transform.rotation = Quaternion.Euler(0, 0, signedAngle);
    }
}
