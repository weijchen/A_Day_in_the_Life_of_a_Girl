using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class ClickEvent : UnityEvent<string>
{

}

[System.Serializable]
public class ZoomEvent : UnityEvent<ZoomProperties>
{

}

public struct ZoomProperties
{
    public Transform zoomInPosition;
    public float zoomInSize;
    public float smoothness;
    public float zoomInTime;
    public float zoomInStay;
    public float zoomOutTime;
}

public class Click : MonoBehaviour
{
    [Header("Zoom In")]
    [SerializeField] Transform zoomInPosition;
    [SerializeField][Range(1.0f, 9.8f)] private float zoomInSize;
    [SerializeField] private float smoothness = 0.01f;
    [SerializeField] private float zoomInTime = 1.0f;
    [SerializeField] private float zoomInStay = 2.0f;
    [SerializeField] private float zoomOutTime = 1.0f;
    
    public ClickEvent clickEvent;
    public ZoomEvent zoomEvent;
    public ZoomProperties zoomProperties;
    
    void Start()
    {
        if (clickEvent == null)
        {
            clickEvent = new ClickEvent();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null &&hit.collider.gameObject==this.gameObject)
            {
                clickEvent.Invoke("11");
                zoomProperties.zoomInPosition = zoomInPosition;
                zoomProperties.zoomInSize = zoomInSize;
                zoomProperties.smoothness = smoothness;
                zoomProperties.zoomInTime = zoomInTime;
                zoomProperties.zoomInStay = zoomInStay;
                zoomProperties.zoomOutTime = zoomOutTime;
                zoomEvent.Invoke(zoomProperties);
                
                //Debug.Log(hit.collider.gameObject.name);
            }

        }
    }

    public void Test()
    {
        Debug.Log("Testing");
    }
}
