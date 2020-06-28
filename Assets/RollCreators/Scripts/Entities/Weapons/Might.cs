using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Might : MonoBehaviour
{
    public Weapon weapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy.isDead) return;
            if (weapon.bounceDistance > 0)
            {
                enemy.transform.position += (enemy.transform.position - transform.position).normalized * weapon.bounceDistance;
            }
            enemy.Hit(weapon.damage);
        }
    }
}
