﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Improvement : MonoBehaviour
{
    public string name;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Game game = GameObject.Find("Game").GetComponent<Game>();
            game.ApplyImprovement(this);
            GameUI ui = FindObjectOfType<GameUI>();
            ui.ShowBonus(this);
            DestroyImmediate(this);
        }
    }
}
