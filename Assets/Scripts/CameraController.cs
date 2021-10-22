using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ZoomProperties
{
    public Transform zoomInPosition;
    public float zoomInSize;
    public float smoothness;
    public float zoomInTime;
    public float zoomInStay;
    public float zoomOutTime;
}

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private float startZoomIn;
    private float startX;
    private float startY;
    private float startZ;
    private Transform originPos;
    private float originSize;

    void Start()
    {
        _camera = GetComponent<Camera>();
        startZoomIn = _camera.orthographicSize;
        startX = gameObject.transform.position.x;
        startY = gameObject.transform.position.y;
        startZ = gameObject.transform.position.z;
        originPos = _camera.transform;
        originSize = 9.6f;
    }

    public void ZoomIn(ZoomProperties zoomProperties)
    {
        StartCoroutine(StartZoomIn(zoomProperties));
    }
    
    IEnumerator StartZoomIn(ZoomProperties zoomProperties)
    {
        float progress = 0;
        float inc = zoomProperties.smoothness / zoomProperties.zoomInTime;

        while (progress < zoomProperties.zoomInStay/zoomProperties.zoomInTime)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomProperties.zoomInSize, progress);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, zoomProperties.zoomInPosition.position, progress);
            progress += inc;
            if (progress >= zoomProperties.zoomInStay/zoomProperties.zoomInTime)
            {
                //ZoomOut(zoomProperties.smoothness, zoomProperties.zoomOutTime);
                RestPos(0);
            }    
            yield return new WaitForSeconds(zoomProperties.smoothness);
        }
    }

    public void ZoomOut(float smoothness, float zoomOutTime)
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

    public void RestPos(float time)
    {
        StartCoroutine(ResetPosIE(time));
    }

    IEnumerator ResetPosIE(float time)
    {
        Debug.Log(originPos.position);
        yield return new WaitForSeconds(time);
        _camera.transform.position = new Vector3(0,0,-10);
        _camera.orthographicSize = originSize;
    }
}
