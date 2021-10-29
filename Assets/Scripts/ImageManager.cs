using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    [SerializeField] private bool usePP = false;
    [SerializeField] private int openPPIndex = 0;
    [SerializeField] private int closePPIndex = 0;
    [SerializeField] private GameObject ppObject;
    
    private int currentindex;
    private bool canFadeNext = true;
    private static Transform[] Images;
    
    private void Awake()
    {
        Images = new Transform[transform.childCount];
        for (int i = 0; i < Images.Length; i++)
        {
            Images[i] = transform.GetChild(i);
        }
    }
    private void Start()
    {
        currentindex = 0;
        ppObject.SetActive(false);
    }

    private void Update()
    {
        if (usePP)
        {
            if (currentindex == openPPIndex)
            {
                ppObject.SetActive(true);
            } else if (currentindex == closePPIndex)
            {
                ppObject.SetActive(false);
            }
            
        }
    }

    public void ChangeToNext(float waitTime)
    {
        StartCoroutine(ChangeToNextIE(waitTime));
    }

    public void FadeToNext(float waitTime)
    {
        canFadeNext = false;
        StartCoroutine(FadeToNextIE(waitTime));
    }

    public void AddIndex()
    {
        currentindex++;
    }

    public void StartFadeIn(GameObject i)
    {
        StartCoroutine(FadeIn(i, 0.01f, 2f));
    }

    public void StartFadeOut(GameObject i)
    {
        i.SetActive(false);
        // StartCoroutine(FadeOut(i, 0.01f, 2f));  // not using right now
    }
    
    // not using right now
    IEnumerator FadeOut(GameObject i, float smoothness, float duration)
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        SpriteRenderer sr = i.GetComponent<SpriteRenderer>();
        while (progress <= 1)
        {
            sr.color = Color.Lerp(new Color(sr.color.r, sr.color.g, sr.color.b, 1), new Color(sr.color.r, sr.color.g, sr.color.b, 0), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
            if (progress >= 1.0f)
            {
                canFadeNext = true;
            }
        }
        i.SetActive(false);
    }

    IEnumerator FadeIn(GameObject i, float smoothness, float duration)
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        SpriteRenderer sr = i.GetComponent<SpriteRenderer>();
        while (progress < 1)
        {
            if (sr != null)
            {
                sr.color = Color.Lerp(new Color(sr.color.r, sr.color.g, sr.color.b, 0), new Color(sr.color.r, sr.color.g, sr.color.b, 1), progress);
                progress += increment;
                yield return new WaitForSeconds(smoothness);
                if (progress >= 1)
                {
                    canFadeNext = true;
                }
            }
            else progress = 1;
            
        }
    }

    IEnumerator FadeToNextIE(float time)
    {
        yield return new WaitForSeconds(time);
        StartFadeOut(Images[currentindex].gameObject);
        foreach (Transform child in Images[currentindex].transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                StartFadeOut(child.gameObject);
            }
        }
        AddIndex();
        if (currentindex < Images.Length)
        {
            Images[currentindex].gameObject.SetActive(true);
            foreach (Transform child in Images[currentindex].transform)
            {
                if (child.GetComponent<SpriteRenderer>() != null)
                {
                    StartFadeIn(child.gameObject);
                }
            }
            StartFadeIn(Images[currentindex].gameObject);    
        }
    }

    IEnumerator ChangeToNextIE(float time)
    {
        yield return new WaitForSeconds(time);
        Images[currentindex].gameObject.SetActive(false);
        AddIndex();
        Images[currentindex].gameObject.SetActive(true);
    }

    public bool GetCanFadeNext()
    {
        return canFadeNext;
    }

    public int GetCurrentIndex()
    {
        return currentindex;
    }
}
