using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Might : MonoBehaviour
{
    public Weapon weapon;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Hit(weapon.damage);
        }
    }
}
