using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezing : MonoBehaviour
{
    private HashSet<Enemy> frozen = new HashSet<Enemy>();
    void Start()
    {
        Destroy(gameObject, 7);
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in frozen)
        {
            enemy.isFrozen = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isFrozen = true;
            frozen.Add(enemy);
        }
    }
}
