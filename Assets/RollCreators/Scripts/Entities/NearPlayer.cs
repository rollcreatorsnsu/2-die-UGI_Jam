using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearPlayer : MonoBehaviour
{
    public Weapon currentWeapon;
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

    public void Attack()
    {
        if (fireRate > 0) return;
        animator.Play(currentWeapon.name);
        fireRate = currentWeapon.rateOfFire;
    }

}
