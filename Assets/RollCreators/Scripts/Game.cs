using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public bool isPaused = false;
    private int _points;

    public int points
    {
        get => _points;
        set
        {
            int diff = value - _points;
            if (doubleUpCount > 0)
            {
                diff *= 2;
            }

            _points += diff;
        }
    }

    public float health = 100;
    public FarPlayer farPlayer;
    public NearPlayer nearPlayer;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private InputController inputController;
    [SerializeField] private GameObject bigExplosion;
    [SerializeField] private GameObject freezing;
    [SerializeField] private ImprovementFactory factory;
    private int doubleUpCount = 0;
    private static int HORIZONTAL_MODEL_SIZE = 20;//138;
    private static int VERTICAL_MODEL_SIZE = 20;//77;

    void Start()
    {
        _points = 0;
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
            GameObject e = Instantiate(enemies[Random.Range(0, enemies.Count)]);
            Enemy enemy = e.GetComponent<Enemy>();
            enemy.game = this;
            enemy.improvementFactory = factory;
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

    public void ApplyImprovement(Improvement improvement, Vector3 position)
    {
        if (improvement as FarWeaponItem)
        {
            FarWeaponItem item = (FarWeaponItem) improvement;
            farPlayer.currentWeapon = item.weapon;
            farPlayer.ResetAnimation();
        }
        else if (improvement as NearWeaponItem)
        {
            NearWeaponItem item = (NearWeaponItem) improvement;
            nearPlayer.currentWeapon = item.weapon;
            nearPlayer.ResetAnimation();
        }
        else if (improvement as SpeedUp)
        {
            inputController.speed += 4;
            StartCoroutine(CancelSpeedUp());
        }
        else if (improvement as HealUp)
        {
            health += 30;
            if (health > 100)
            {
                health = 100;
            }
        }
        else if (improvement as DoubleUp)
        {
            doubleUpCount++;
            StartCoroutine(CancelDoubleUp());
        }
        else if (improvement as BlowUp)
        {
            Instantiate(bigExplosion, position, Quaternion.identity);
        }
        else if (improvement as FreezeUp)
        {
            Instantiate(freezing, position, Quaternion.identity);
        }
    }

    private IEnumerator CancelSpeedUp()
    {
        yield return new WaitForSeconds(15);
        inputController.speed -= 4;
    }

    private IEnumerator CancelDoubleUp()
    {
        yield return new WaitForSeconds(30);
        doubleUpCount--;
    }

}
