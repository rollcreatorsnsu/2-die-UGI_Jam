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
        float randomCoord = Random.Range(-Game.EDGE_OF_MAP, Game.EDGE_OF_MAP);
        switch (Random.Range(0, 4))
        {
            case 0:
                transform.position = new Vector3(randomCoord, Game.EDGE_OF_MAP);
                break;
            case 1:
                transform.position = new Vector3(-Game.EDGE_OF_MAP, randomCoord);
                break;
            case 2:
                transform.position = new Vector3(randomCoord, -Game.EDGE_OF_MAP);
                break;
            case 3:
                transform.position = new Vector3(Game.EDGE_OF_MAP, randomCoord);
                break;
        }
    }

    void Update()
    {
        if (isDead) return;
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
        if (other.gameObject.CompareTag("Player") && !isDead)
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Hit(attackPerFrame);
        }
    }
}
