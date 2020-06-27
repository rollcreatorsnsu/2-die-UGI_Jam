using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon weapon;
    public Vector3 direction;

    void Update()
    {
        transform.position += direction * weapon.speed;
        if (Vector3.Distance(Vector3.zero, transform.position) > Game.EDGE_OF_MAP)
        {
            DestroyImmediate(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Hit(weapon.damage);
            DestroyImmediate(this);
        }
    }
}
