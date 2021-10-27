using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraPosition : MonoBehaviour
{
    [SerializeField] private Camera cam;
    
    private Vector3 originPosition = Vector3.zero;
    private bool hasReset = false;

    void Start()
    {
        originPosition = new Vector3(0, 0, -10);
        ResetCamera();
    }

    private void Update()
    {
        if (cam.transform.position != originPosition)
        {
            ResetCamera();
            hasReset = true;
        }
    }

    void ResetCamera()
    {
        cam.transform.position = originPosition;
        cam.orthographicSize = 9.6f;
    }
}
