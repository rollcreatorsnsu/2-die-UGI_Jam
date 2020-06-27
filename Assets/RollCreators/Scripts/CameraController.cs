﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 3;
    [SerializeField] private float panDetect = 15;
    
    void Update()
    {
        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;
        if (xPos < panDetect)
        {
            transform.position += new Vector3(-panSpeed, 0);
            if (transform.position.x < -Game.EDGE_OF_MAP)
            {
                transform.position = new Vector3(-Game.EDGE_OF_MAP, transform.position.y);
            }
        }
        if (xPos > Screen.width - panDetect)
        {
            transform.position += new Vector3(panSpeed, 0);
            if (transform.position.x > Game.EDGE_OF_MAP)
            {
                transform.position = new Vector3(Game.EDGE_OF_MAP, transform.position.y);
            }
        }
        if (yPos < panDetect)
        {
            transform.position += new Vector3(0, -panSpeed);
            if (transform.position.y < -Game.EDGE_OF_MAP)
            {
                transform.position = new Vector3(transform.position.x, -Game.EDGE_OF_MAP);
            }
        }
        if (yPos > Screen.height - panDetect)
        {
            transform.position += new Vector3(0, panSpeed);
            if (transform.position.y > Game.EDGE_OF_MAP)
            {
                transform.position = new Vector3(transform.position.x, Game.EDGE_OF_MAP);
            }
        }
    }
}
