using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Improvement : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = gameObject.GetComponent<Player>();
            player.ApplyImprovement(this);
            DestroyImmediate(this);
        }
    }
}
