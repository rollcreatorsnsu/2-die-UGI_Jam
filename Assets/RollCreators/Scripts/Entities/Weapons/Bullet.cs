using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Game game;
    public Weapon weapon;
    public Vector3 direction;
    [SerializeField] private GameObject explosionPrefab;

    void Update()
    {
        if (game.isPaused) return;
        transform.position += direction * weapon.speed * Time.deltaTime;
        float signedAngle = Vector2.SignedAngle(Vector2.up, direction);
        transform.rotation = Quaternion.Euler(0, 0, signedAngle);
        if (Vector3.Distance(Vector3.zero, transform.position) > game.GetVerticalSize() * game.GetHorizontalSize())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy.isDead) return;
            enemy.Hit(weapon.damage);
            if (weapon.isExplodable)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            if (weapon.fireDamagePercent > 0)
            {
                StartCoroutine(enemy.Fire(weapon.fireDamageRate, weapon.fireDamagePercent));
            }

            if (weapon.bounceDistance > 0)
            {
                enemy.transform.position += (enemy.transform.position - transform.position).normalized * weapon.bounceDistance;
            }
            Destroy(gameObject);
        }
    }
}
