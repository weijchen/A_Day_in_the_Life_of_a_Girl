using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntroEvent : UnityEvent<string>
{

}

public class IntroBtn : MonoBehaviour
{
    [SerializeField] private AudioClip clickAudioClip;
    public IntroEvent introEvent;

    private SoundMgr _soundMgr;

    void Start()
    {
        _soundMgr = FindObjectOfType<SoundMgr>();
        if (introEvent == null)
        {
            introEvent = new IntroEvent();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                _soundMgr.PlaySoundFromClick(clickAudioClip);
                introEvent.Invoke("11");
            }
        }
    }
}
