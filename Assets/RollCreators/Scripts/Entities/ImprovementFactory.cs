using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovementFactory : MonoBehaviour
{
    [SerializeField] private List<GameObject> improvements;
    
    public void CreateImprovement(Vector3 position)
    {
        Instantiate(improvements[Random.Range(0, improvements.Count)], position, Quaternion.identity);
    }
}
