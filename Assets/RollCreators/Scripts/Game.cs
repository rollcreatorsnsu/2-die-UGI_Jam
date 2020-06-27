using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public bool isPaused = false;
    public int points = 0;
    public float health = 100;
    public FarPlayer farPlayer;
    public NearPlayer nearPlayer;
    [SerializeField] private List<GameObject> enemies;
    private static int HORIZONTAL_MODEL_SIZE = 138;
    private static int VERTICAL_MODEL_SIZE = 77; 

    void Start()
    {
        points = 0;
        health = 100;
        farPlayer.currentWeapon = new Weapon
        {
            name = "Pistol",
            damage = 10,
            rateOfFire = 300,
            spread = 1,
            speed = 100
        };
        nearPlayer.currentWeapon = new Weapon
        {
            name = "Sword",
            damage = 15,
            rateOfFire = 250,
            spread = 50,
            speed = 5
        };
        StartCoroutine(SpawnEnemy());
    }

    public float GetHorizontalSize()
    {
        return HORIZONTAL_MODEL_SIZE / 2;
    }

    public float GetVerticalSize()
    {
        return VERTICAL_MODEL_SIZE / 2;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(5); // TODO: Balance
            if (isPaused) continue;
            Instantiate(enemies[Random.Range(0, enemies.Count)]);
        }
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            farPlayer.Die();
            nearPlayer.Die();
        }
    }

    public void ApplyImprovement(Improvement improvement)
    {
        if (improvement as FarWeaponItem)
        {
            FarWeaponItem item = (FarWeaponItem) improvement;
            farPlayer.currentWeapon = item.weapon;
        }
        else if (improvement as NearWeaponItem)
        {
            NearWeaponItem item = (NearWeaponItem) improvement;
            nearPlayer.currentWeapon = item.weapon;
        }
    }
}
