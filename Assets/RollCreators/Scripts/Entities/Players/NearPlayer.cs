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
    [SerializeField] private AudioSource swordSound;
    [SerializeField] private AudioSource flameSound;
    [SerializeField] private AudioSource batSound;

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
        if (currentWeapon.name == "Flamethrower")
        {
            animator.Play($"Attack_{currentWeapon.name}");
        }
        else
        {
            animator.Play($"Attack_{currentWeapon.name}_{Random.Range(1, 3)}");
        }

        switch (currentWeapon.name)
        {
            case "Sword":
                swordSound.Play();
                break;
            case "Flamethrower":
                flameSound.Play();
                break;
            case "Bat":
                batSound.Play();
                break;
        }
        fireRate = currentWeapon.rateOfFire / 1000f;
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
