using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarPlayer : MonoBehaviour
{
    public Weapon currentWeapon;
    [SerializeField] private GameObject emptyBullet;
    [SerializeField] private Game game;
    private Animator animator;
    private float fireRate = 0;
    
    void Start()
    {
        animator = GetComponent<Animator>();
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
        GameObject bulletObject = Instantiate(emptyBullet);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.game = game;
        bullet.weapon = currentWeapon;
        bullet.direction = Quaternion.Euler(0, 0, Random.Range(-currentWeapon.spread, currentWeapon.spread)) * (transform.position - position).normalized;
        fireRate = currentWeapon.rateOfFire;
    }

    public void Die()
    {
        
    }
}
