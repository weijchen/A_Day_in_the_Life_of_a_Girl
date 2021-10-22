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
    [Header("General")] 
    [SerializeField] private bool hasStartDelay = false;
    [SerializeField] private float startDelayTime = 1.0f;
    
    [Header("Zoom In")]
    [SerializeField] Transform zoomInPosition;
    [SerializeField][Range(1.0f, 9.8f)] private float zoomInSize;
    [SerializeField] private float smoothness = 0.01f;
    [SerializeField] private float zoomInTime = 1.0f;
    [SerializeField] private float zoomInStay = 2.0f;
    [SerializeField] private float zoomOutTime = 1.0f;
    [SerializeField] private GameObject textBubble;
    [SerializeField] private GameObject textContext;
    [SerializeField] private bool fadeToNext = false;
    [SerializeField] private bool chgToNext = false;
    [SerializeField] private float priorDeleteTime = 0.2f;

    public ClickEvent clickEvent;
    public ZoomEvent zoomEvent;
    public ZoomProperties zoomProperties;

    private float delayTimer = 0f;
    private ImageManager _imageManager;
    
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _imageManager = FindObjectOfType<ImageManager>();
        
        if (clickEvent == null)
        {
            clickEvent = new ClickEvent();
        }

        if (zoomEvent == null)
        {
            zoomEvent = new ZoomEvent();
        }

        if (textBubble != null)
        {
            textBubble.SetActive(false);
            textContext.SetActive(false);

        }
    }
    
    void Update()
    {
        if (hasStartDelay)
        {
            if (delayTimer < startDelayTime)
            {
                if (textBubble != null)
                {
                    textBubble.SetActive(false);
                    textContext.SetActive(false);
                }
                delayTimer += Time.deltaTime;
            } 
            else
            {
                if (textBubble != null)
                {
                    textBubble.SetActive(true);
                }
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            if (textBubble != null)
            {
                textBubble.SetActive(true);
            }
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject && _imageManager.GetCanFadeNext())
            {
                if (_imageManager.GetCanFadeNext())
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
                    if (textContext != null)
                    {
                        textContext.SetActive(true);
                    }

                    if (fadeToNext)
                    {
                        _imageManager.FadeToNext(zoomOutTime);
                        Destroy(textBubble, zoomOutTime - priorDeleteTime);
                        Destroy(textContext, zoomOutTime - priorDeleteTime);
                    }
                    
                    if (chgToNext)
                    {
                        _imageManager.ChangeToNext(zoomOutTime);
                        Destroy(textBubble, zoomOutTime - priorDeleteTime);                        Destroy(textBubble, zoomOutTime - priorDeleteTime);
                        Destroy(textContext, zoomOutTime - priorDeleteTime);
                    }
                }

                if (!chgToNext && delayTimer >= startDelayTime)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
