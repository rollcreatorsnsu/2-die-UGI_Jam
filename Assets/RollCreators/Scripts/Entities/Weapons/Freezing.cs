using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezing : MonoBehaviour
{
    void Start()
    {
        Destroy(this, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isFrozen = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isFrozen = false;
        }
    }
}
