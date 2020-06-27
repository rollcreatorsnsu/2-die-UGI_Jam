using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Might : MonoBehaviour
{
    void Start()
    {
        Destroy(this, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isFrozen = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isFrozen = false;
        }
    }
}
