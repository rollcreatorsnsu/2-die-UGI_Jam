using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Game game;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit(float damage)
    {
        game.health -= damage;
        if (game.health < 0)
        {
            animator.Play("Die");
        }
    }

    public void ApplyImprovement(Improvement improvement)
    {
        //TODO: improvements logic
    }
    
}
