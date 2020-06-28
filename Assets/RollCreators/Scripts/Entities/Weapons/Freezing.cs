using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezing : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 7);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isFrozen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isFrozen = false;
        }
    }
}
