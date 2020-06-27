using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Game game;
    public ImprovementFactory improvementFactory;
    [SerializeField] private float health;
    [SerializeField] private int points;
    [SerializeField] private float speed;
    [SerializeField] private float attackPerFrame;
    private GameObject[] players;
    private Animator animator;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomStartPosition();
        players = GameObject.FindGameObjectsWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void SetRandomStartPosition()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                transform.position = new Vector3(Random.Range(-game.GetHorizontalSize(), game.GetHorizontalSize()), game.GetVerticalSize());
                break;
            case 1:
                transform.position = new Vector3(-game.GetHorizontalSize(), Random.Range(-game.GetVerticalSize(), game.GetVerticalSize()));
                break;
            case 2:
                transform.position = new Vector3(Random.Range(-game.GetHorizontalSize(), game.GetHorizontalSize()), -game.GetVerticalSize());
                break;
            case 3:
                transform.position = new Vector3(game.GetHorizontalSize(), Random.Range(-game.GetVerticalSize(), game.GetVerticalSize()));
                break;
        }
    }

    void Update()
    {
        if (isDead || game.isPaused) return;
        Vector3 nearestPlayerCoord = transform.position;
        float nearestDistance = Single.MaxValue;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPlayerCoord = player.transform.position;
            }
        }

        transform.position += (transform.position - nearestPlayerCoord).normalized * speed;
        transform.LookAt(nearestPlayerCoord);
    }

    public void Hit(float damage)
    {
        health -= damage;
        CheckDead();
    }

    public IEnumerator Fire(float rate, float percentDamage)
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(rate);
            if (game.isPaused) continue;
            health -= health * percentDamage;
            CheckDead();
        }
    }

    private void CheckDead()
    {
        if (health < 0)
        {
            isDead = true;
            game.points += points;
            animator.Play("Die");
            if (Random.value > 0.5) // TODO: balance
            {
                improvementFactory.CreateImprovement(transform.position);
            }
            Destroy(this, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isDead && !game.isPaused)
        {
            game.Hit(attackPerFrame);
        }
    }
}
