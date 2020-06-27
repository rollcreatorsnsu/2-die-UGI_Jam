using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionDamage;
    void Start()
    {
        Destroy(this, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Hit(explosionDamage);
        }
    }
}
