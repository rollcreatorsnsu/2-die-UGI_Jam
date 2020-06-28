using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovementFactory : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private GameObject speedUp;
    [SerializeField] private GameObject healUp;
    [SerializeField] private GameObject doubleUp;
    [SerializeField] private GameObject blowUp;
    [SerializeField] private GameObject freezeUp;
    [SerializeField] private List<GameObject> weaponsUp;
    
    public void CreateImprovement(Vector3 position)
    {
        GameObject currentImprovement = null;
        float rand = Random.value;
        if (rand < 0.2)
        {
            currentImprovement = speedUp;
        }
        else if (rand < 0.4)
        {
            currentImprovement = healUp;
        }
        else if (rand < 0.5)
        {
            currentImprovement = doubleUp;
        }
        else if (rand < 0.6)
        {
            currentImprovement = blowUp;
        }
        else if (rand < 0.8)
        {
            currentImprovement = freezeUp;
        }
        else
        {
            while (true)
            {
                currentImprovement = weaponsUp[Random.Range(0, weaponsUp.Count)];
                string weapon = currentImprovement.GetComponent<Improvement>().name;
                if (weapon != game.farPlayer.currentWeapon.name && weapon != game.nearPlayer.currentWeapon.name)
                {
                    break;
                }
            }
        }
        Instantiate(currentImprovement, position, Quaternion.identity);
        
    }
}
