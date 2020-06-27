using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int EDGE_OF_MAP = 1000;
    public int points = 0;
    public float health = 100;

    void Start()
    {
        points = 0;
        health = 100;
    }

    public static void GameOver()
    {
        //TODO: UI logic
    }
}
