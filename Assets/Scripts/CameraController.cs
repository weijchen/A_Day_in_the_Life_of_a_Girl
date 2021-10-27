using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public struct ZoomProperties
{
    public Transform zoomInPosition;
    public float zoomInSize;
    public float smoothness;
    public float zoomInTime;
    public float zoomOutTime;
    public float zoomInStay;
    public TransitionType zoomOutType;
    public bool chgAfterZoomIn;
}

public enum TransitionType
{
    [Description("Change")]
    change = 1,
    [Description("Fade")]
    fade = 2
}

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private float startZoomIn;
    private float startX;
    private float startY;
    private float startZ;
    private Transform originPos;
    private float originSize = 9.6f;
    private static bool canZoom;
    private bool isZoomOutFinish = true;
    private ImageManager _imageManager;

    void Start()
    {
        _camera = GetComponent<Camera>();
        startZoomIn = _camera.orthographicSize;
        startX = gameObject.transform.position.x;
        startY = gameObject.transform.position.y;
        startZ = gameObject.transform.position.z;
        originPos = _camera.transform;
        _imageManager = FindObjectOfType<ImageManager>();
    }

    public void ZoomIn(ZoomProperties zoomProperties)
    {
        StartCoroutine(StartZoomIn(zoomProperties));
    }

    public void ZoomInWithZoomOut(ZoomProperties zoomProperties)
    {
        StartCoroutine(StartZoomInWithZoomOut(zoomProperties));
    }

    IEnumerator StartZoomInWithZoomOut(ZoomProperties zoomProperties)
    {
        StartCoroutine(StartZoomIn(zoomProperties));
        yield return new WaitForSeconds(zoomProperties.zoomInStay);
        StartCoroutine(StartZoomOut(zoomProperties));
    }
    
    IEnumerator StartZoomIn(ZoomProperties zoomProperties)
    {
        float progress = 0;
        float inc = zoomProperties.smoothness;
        while (progress < zoomProperties.zoomInTime)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomProperties.zoomInSize, progress);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, zoomProperties.zoomInPosition.position, progress);
            progress += inc;
            if (progress >= zoomProperties.zoomInTime)
            {
                if (zoomProperties.chgAfterZoomIn)
                {
                    if (zoomProperties.zoomOutType == TransitionType.change)
                    {
                        _imageManager.ChangeToNext(0);
                    } 
                    else if (zoomProperties.zoomOutType == TransitionType.fade)
                    {
                        _imageManager.FadeToNext(0);
                    }
                }
            //     if (zoomOut)
            //     {
            //         ZoomOut(zoomProperties.smoothness, zoomProperties.zoomOutTime);
            //     }
            //     else
            //     {
            //         RestPos(0);
            //     }
            }
            yield return new WaitForSeconds(zoomProperties.smoothness);
        }
    }

    public void ZoomOut(ZoomProperties zoomProperties)
    {
        isZoomOutFinish = false;
        StartCoroutine(StartZoomOut(zoomProperties));
    }

    IEnumerator StartZoomOut(ZoomProperties zoomProperties)
    {
        float progress = 0;
        float inc = zoomProperties.smoothness;
        while (progress < zoomProperties.zoomOutTime)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, startZoomIn, progress);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(startX, startY, startZ), progress);
            progress += inc;
            
            yield return new WaitForSeconds(zoomProperties.smoothness);

            if (progress >= zoomProperties.zoomOutTime)
            {
                if (zoomProperties.zoomOutType == TransitionType.change)
                {
                    _imageManager.ChangeToNext(0);
                } else if (zoomProperties.zoomOutType == TransitionType.fade)
                {
                    _imageManager.FadeToNext(0);
                }
            }
        }
    }

    public void RestPos(float time)
    {
        StartCoroutine(ResetPosIE(time));
    }

    IEnumerator ResetPosIE(float time)
    {
        yield return new WaitForSeconds(time);
        _camera.transform.position = new Vector3(0,0,-10);
        _camera.orthographicSize = originSize;
    }

    public bool GetIsZoomOutFinish()
    {
        return isZoomOutFinish;
    }
}
