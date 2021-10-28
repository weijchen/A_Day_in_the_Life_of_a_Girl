using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousScene : MonoBehaviour
{
    [SerializeField] private List<GameObject> scenes;
    [SerializeField] private float timeToChange = 2.0f;
    [SerializeField] private GameObject[] bordersToOpen;
    [SerializeField] private ArgumentSpawner argumentSpawner;
    [SerializeField] private bool chgMaterial = false;

    private float timer;
    private int currIndex = 0;
    private int totalIndex = 0;
    
    void Start()
    {
        totalIndex = scenes.Count;
        foreach (GameObject o in bordersToOpen)
        {
            o.SetActive(false);
        }
    }

    void Update()
    {
        SceneSwitcher();
    }

    private void SceneSwitcher()
    {
        Debug.Log(totalIndex);
        Debug.Log(currIndex);
        
        if (chgMaterial)
        {
            Debug.Log("chg");
            if (currIndex == totalIndex - 1)
            {
                Debug.Log("chgchg");
                argumentSpawner.ChgArgumentPrefab();
            }
        }
        
        if (currIndex < totalIndex - 1)
        {
            if (timer <= timeToChange)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f;
                scenes[currIndex].gameObject.SetActive(false);
                currIndex += 1;
            }
        }
        else
        {
            OpenBorders();
        }
    }

    private void OpenBorders()
    {
        foreach (GameObject o in bordersToOpen)
        {
            o.SetActive(true);
        }
    }
}
