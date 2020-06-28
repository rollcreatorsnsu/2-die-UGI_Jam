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
    public bool isFrozen;
    [SerializeField] private float health;
    [SerializeField] private int points;
    [SerializeField] private float speed;
    [SerializeField] private float attackPerFrame;
    [SerializeField] private string name;
    private AudioSource deadSound;
    private GameObject[] players;
    private Animator animator;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        deadSound = game.enemyDeadSound;
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
        if (isDead || game.isPaused || isFrozen) return;
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

        transform.position += (nearestPlayerCoord - transform.position).normalized * speed * Time.deltaTime;
        float signedAngle = Vector2.SignedAngle(Vector2.up, nearestPlayerCoord - transform.position);
        transform.rotation = Quaternion.Euler(0, 0, signedAngle);
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
            deadSound.Play();
            isDead = true;
            game.points += points;
            animator.Play("Die");
            if (Random.value < 0.1)
            {
                improvementFactory.CreateImprovement(transform.position);
            }
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isDead && !game.isPaused && !isFrozen)
        {
            animator.Play("Move");
            switch (name)
            {
                case "Imp":
                    if (!game.impSound.isPlaying)
                    {
                        game.impSound.Play();
                    }
                    break;
                case "Hunter":
                    if (!game.hunterSound.isPlaying)
                    {
                        game.hunterSound.Play();
                    }
                    break;
                case "Devil":
                    if (!game.devilSound.isPlaying)
                    {
                        game.devilSound.Play();
                    }
                    break;
            }
            game.Hit(attackPerFrame);
        }
    }
}
