using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousScene : MonoBehaviour
{
    [SerializeField] private List<GameObject> scenes;
    [SerializeField] private float timeToChange = 2.0f;

    private float timer;
    private int currIndex = 0;
    private int totalIndex = 0;
    
    void Start()
    {
        totalIndex = scenes.Count;
    }

    void Update()
    {
        SceneSwitcher();
    }

    private void SceneSwitcher()
    {
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
    }
}
