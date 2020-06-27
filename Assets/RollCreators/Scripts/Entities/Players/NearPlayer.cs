using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearPlayer : MonoBehaviour
{
    public Weapon currentWeapon;
    private Animator animator;
    private float fireRate = 0;
    [SerializeField] private Game game;
    [SerializeField] private GameUI ui;

    void Start()
    {
        animator = GetComponent<Animator>();
        ResetAnimation();
    }
    
    void Update()
    {
        if (game.isPaused) return;
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (fireRate > 0) return;
        animator.Play($"Shot_{currentWeapon.name}");
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
