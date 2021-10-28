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
    [SerializeField] private AudioClip clickAudioClip;
    
    [Header("Zoom In")]
    [SerializeField] Transform zoomInPosition;
    [SerializeField][Range(1.0f, 9.8f)] private float zoomInSize;
    [SerializeField] private float smoothness = 0.01f;
    [SerializeField] private float zoomInTime = 1.0f;
    [SerializeField] private float zoomOutTime = 1.0f;
    [SerializeField] private float zoomInStay = 1.0f;
    [SerializeField] private TransitionType zoomOutType = TransitionType.change;
    [SerializeField] private bool chgAfterZoomIn = false;
    [SerializeField] private GameObject textBubble;
    [SerializeField] private GameObject textContext;

    public ClickEvent clickEvent;
    public ZoomEvent zoomEvent;
    public ZoomProperties zoomProperties;

    private bool hasZoom = false;
    private float delayTimer = 0f;
    private ImageManager _imageManager;
    private CameraController _cameraController;
    private SoundMgr _soundMgr;
    
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _imageManager = FindObjectOfType<ImageManager>();
        _cameraController = FindObjectOfType<CameraController>();
        _soundMgr = FindObjectOfType<SoundMgr>();
        
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
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//Debug.Log(hit.collider.gameObject.name);
            if (hit.collider != null && hit.collider.gameObject == gameObject && _imageManager.GetCanFadeNext())
            {
                _soundMgr.PlaySoundFromClick(clickAudioClip);
                clickEvent.Invoke("11");
                GetComponent<BoxCollider2D>().enabled = false;
                
                if (!hasZoom && (zoomInPosition != null || _cameraController.GetIsZoomOutFinish()))
                {
                    zoomProperties.zoomInPosition = zoomInPosition;
                    zoomProperties.zoomInSize = zoomInSize;
                    zoomProperties.smoothness = smoothness;
                    zoomProperties.zoomInTime = zoomInTime;
                    zoomProperties.zoomOutTime = zoomOutTime;
                    zoomProperties.zoomInStay = zoomInStay;
                    zoomProperties.zoomOutType = zoomOutType;
                    zoomProperties.chgAfterZoomIn = chgAfterZoomIn;
                    zoomEvent.Invoke(zoomProperties);
                    if (textContext != null)
                    {
                        textContext.SetActive(true);
                    }

                    hasZoom = true;
                }

                if (delayTimer >= startDelayTime)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
