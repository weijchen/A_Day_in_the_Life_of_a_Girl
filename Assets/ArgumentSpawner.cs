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
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private bool isUp = false;
    [SerializeField] private bool autoDestroy = false;
    [SerializeField] private GameObject destroyBase;

    private ArgumentPoints _argumentPoints;
    private ArgumentPointsUp _argumentPointsUp;
    private List<Transform> pickedPath;
    private GameObject spawnPrefab;

    private int currIndex = 0;
    private float timer = 0;

    private void Start()
    {
        if (isUp)
        {
            _argumentPoints = null;
            _argumentPointsUp = FindObjectOfType<ArgumentPointsUp>();
            pickedPath = _argumentPointsUp.GetPoints();
        }
        else
        {
            _argumentPoints = FindObjectOfType<ArgumentPoints>();
            _argumentPointsUp = null;
            pickedPath = _argumentPoints.GetPoints();
        }
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

                spawnPrefab = Instantiate(argumentPrefab, spawnTransform.position, rot);
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
        if (autoDestroy)
        {
            if (Mathf.Abs(i.transform.position.y - destroyBase.transform.position.y) <= 0.5f)
            {
                Destroy(i);
            }
        }
    }
}
