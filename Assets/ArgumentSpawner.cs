using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgumentSpawner : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float initialSpawnTime = 1.0f;
    [SerializeField] private float decreaseScale = 0.05f;
    [SerializeField] private GameObject argumentPrefab;
    [SerializeField] private GameObject nextArrow;
    
    private ArgumentPoints _argumentPoints;
    private List<Transform> pickedPath;
    private GameObject spawnPrefab;

    private int currIndex = 0;
    private float timer = 0;

    private void Start()
    {
        _argumentPoints = FindObjectOfType<ArgumentPoints>();
        pickedPath = _argumentPoints.GetPoints();
        nextArrow.SetActive(false);
    }

    private void Update()
    {
        if (currIndex < pickedPath.Count - 1)
        {
            if (timer < initialSpawnTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Quaternion rot = transform.rotation;
                if (currIndex % 2 == 1)
                {
                    rot.y += 180.0f;
                }

                spawnPrefab = Instantiate(argumentPrefab, transform.position, rot);
                spawnPrefab.transform.parent = transform;
                timer = 0;
                initialSpawnTime -= decreaseScale;
                currIndex += 1;
            }
        }
        else
        {
            nextArrow.SetActive(true);
            nextArrow.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (spawnPrefab)
        {
            Move(spawnPrefab);
        }
    }

    private void Move(GameObject i)
    {
        i.transform.position = Vector3.MoveTowards(i.transform.position, pickedPath[currIndex].position, moveSpeed * Time.deltaTime);
    }
}
