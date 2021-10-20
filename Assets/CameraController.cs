using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Zoom In")]
    [SerializeField] Transform zoomInPosition;
    [SerializeField][Range(0, 9.8f)] private float zoomInSize;
    [SerializeField] private float smoothness = 0.01f;
    [SerializeField] private float zoomInTime = 1.0f;
    [SerializeField] private float zoomInStay = 2.0f;
    [SerializeField] private float zoomOutTime = 1.0f;

    private Camera _camera;
    private float startZoomIn;
    private float startX;
    private float startY;
    private float startZ;

    void Start()
    {
        _camera = GetComponent<Camera>();
        startZoomIn = _camera.orthographicSize;
        startX = gameObject.transform.position.x;
        startY = gameObject.transform.position.y;
        startZ = gameObject.transform.position.z;
        //ZoomIn();
    }

    public void setZoomInPosition(Transform newPosition)
    {
        zoomInPosition = newPosition;
    }
    
    public void setZoomInSize(float newSize)
    {
        if (newSize >= 0f && newSize <= 5.0f)
        {
            zoomInSize = newSize;
        }
        else
        {
            Debug.LogError("Zoom In Size invalid.");
        }
    }

    public void ZoomIn()
    {
        StartCoroutine(StartZoomIn(smoothness, zoomInTime));
    }
    
    IEnumerator StartZoomIn(float smoothness, float duration)
    {
        float progress = 0;
        float inc = smoothness / duration;

        while (progress < zoomInStay/duration)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomInSize, progress);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, zoomInPosition.position, progress);
            progress += inc;
            if (progress >= zoomInStay/duration)
            {
                //ZoomOut();
            }    
            yield return new WaitForSeconds(smoothness);
        }
    }

    public void ZoomOut()
    {
        StartCoroutine(StartZoomOut(smoothness, zoomOutTime));
    }

    IEnumerator StartZoomOut(float smoothness, float duration)
    {
        float progress = 0;
        float inc = smoothness / duration;

        while (progress < duration)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, startZoomIn, progress);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(startX, startY, startZ), progress);
            progress += inc;
            
            yield return new WaitForSeconds(smoothness);
        }
    }
}
