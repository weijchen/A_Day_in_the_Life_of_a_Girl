using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArgumentPoints : MonoBehaviour
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
