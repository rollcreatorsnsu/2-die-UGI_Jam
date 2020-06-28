using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarPlayer : MonoBehaviour
{
    public Weapon currentWeapon;
    [SerializeField] private GameObject emptyBullet;
    [SerializeField] private Game game;
    [SerializeField] private GameUI ui;
    private Animator animator;
    private float fireRate = 0;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        ResetAnimation();
    }

    void Update()
    {
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
    }

    public void Attack(Vector3 position)
    {
        if (fireRate > 0) return;
        animator.Play($"Shot_{currentWeapon.name}");
        GameObject bulletObject = Instantiate(emptyBullet, transform.position, Quaternion.identity);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.game = game;
        bullet.weapon = currentWeapon;
        bullet.direction = Quaternion.Euler(0, 0, Random.Range(-currentWeapon.spread, currentWeapon.spread)) * (transform.position - position).normalized;
        fireRate = currentWeapon.rateOfFire;
    }

    public void Die()
    {
        animator.Play($"Die_{currentWeapon.name}");
    }

    public void ResetAnimation()
    {
        animator.Play($"Idle_{currentWeapon.name}");
    }

    public void GameOver()
    {
        ui.ShowGameOver();
    }
}
