using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private float panSpeed = 3;
    [SerializeField] private float panDetect = 15;
    
    void Update()
    {
        if (game.isPaused) return;
        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;
        if (xPos < panDetect)
        {
            if (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x >= -game.GetHorizontalSize())
            {
                transform.position += new Vector3(-panSpeed * Time.deltaTime, 0);
            }
        }
        if (xPos > Screen.width - panDetect)
        {
            if (Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x <= game.GetHorizontalSize())
            {
                transform.position += new Vector3(panSpeed * Time.deltaTime, 0);
            }
        }
        if (yPos < panDetect)
        {
            if (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y >= -game.GetVerticalSize())
            {
                transform.position += new Vector3(0, -panSpeed * Time.deltaTime);
            }
        }
        if (yPos > Screen.height - panDetect)
        {
            if (Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y <= game.GetVerticalSize())
            {
                transform.position += new Vector3(0, panSpeed * Time.deltaTime);
            }
        }
    }
}
