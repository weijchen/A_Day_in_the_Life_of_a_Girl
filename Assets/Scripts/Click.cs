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


public class Click : MonoBehaviour
{
    [Header("Zoom In")]
    [SerializeField] Transform zoomInPosition;
    [SerializeField][Range(1.0f, 9.8f)] private float zoomInSize;
    [SerializeField] private float smoothness = 0.01f;
    [SerializeField] private float zoomInTime = 1.0f;
    [SerializeField] private float zoomInStay = 2.0f;
    [SerializeField] private float zoomOutTime = 1.0f;
    [SerializeField] private GameObject textObj;

    [Header("Next Arrow")]
    [SerializeField] private float timeToShow = 3.0f;
    
    public ClickEvent clickEvent;
    public ZoomEvent zoomEvent;
    public ZoomProperties zoomProperties;

    private float timer = 0f;
    private ImageManager _imageManager;
    
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _imageManager = FindObjectOfType<ImageManager>();
        if (clickEvent == null)
        {
            clickEvent = new ClickEvent();
        }

        if (zoomEvent == null)
        {
            zoomEvent = new ZoomEvent();
        }

        if (textObj != null)
        {
            textObj.SetActive(false);
        }
    }
    
    void Update()
    {
        if (timer < timeToShow)
        {
            timer += Time.deltaTime;
        } else if (timer >= timeToShow)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (_imageManager.canFadeNext)
                {
                    clickEvent.Invoke("11");
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                
                if (zoomInPosition != null)
                {
                    zoomProperties.zoomInPosition = zoomInPosition;
                    zoomProperties.zoomInSize = zoomInSize;
                    zoomProperties.smoothness = smoothness;
                    zoomProperties.zoomInTime = zoomInTime;
                    zoomProperties.zoomInStay = zoomInStay;
                    zoomProperties.zoomOutTime = zoomOutTime;
                    zoomEvent.Invoke(zoomProperties);
                    if (textObj != null)
                    {
                        textObj.SetActive(true);
                    }
                }

                if (timer >= timeToShow)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
