using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgumentPointsUp : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    
    void Start()
    {
        foreach (Transform child in transform)
        {
            points.Add(child);
        }
    }

    public List<Transform> GetPoints()
    {
        return points;
    }
}
