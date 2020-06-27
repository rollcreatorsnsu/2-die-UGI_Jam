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
        if (!game.isPaused) return;
        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;
        if (xPos < panDetect)
        {
            transform.position += new Vector3(-panSpeed, 0);
            if (transform.position.x < -game.GetHorizontalSize())
            {
                transform.position = new Vector3(-game.GetHorizontalSize(), transform.position.y);
            }
        }
        if (xPos > Screen.width - panDetect)
        {
            transform.position += new Vector3(panSpeed, 0);
            if (transform.position.x > game.GetHorizontalSize())
            {
                transform.position = new Vector3(game.GetHorizontalSize(), transform.position.y);
            }
        }
        if (yPos < panDetect)
        {
            transform.position += new Vector3(0, -panSpeed);
            if (transform.position.y < -game.GetVerticalSize())
            {
                transform.position = new Vector3(transform.position.x, -game.GetVerticalSize());
            }
        }
        if (yPos > Screen.height - panDetect)
        {
            transform.position += new Vector3(0, panSpeed);
            if (transform.position.y > game.GetVerticalSize())
            {
                transform.position = new Vector3(transform.position.x, game.GetVerticalSize());
            }
        }
    }
}
