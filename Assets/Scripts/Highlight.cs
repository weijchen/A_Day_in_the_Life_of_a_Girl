using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn(this.gameObject,0.01f,1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn(GameObject i, float smoothness, float duration)
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        SpriteRenderer sr = i.GetComponent<SpriteRenderer>();
        while (progress < 1)
        {
            sr.color = Color.Lerp(new Color(sr.color.r, sr.color.g, sr.color.b, 0.2f), new Color(sr.color.r, sr.color.g, sr.color.b, 1), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
            if (progress >= 1)
            {
                StartCoroutine(FadeOut(i,0.01f,1f));
            }
        }
    }

    IEnumerator FadeOut(GameObject i, float smoothness, float duration)
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        SpriteRenderer sr = i.GetComponent<SpriteRenderer>();
        while (progress < 1)
        {
            sr.color = Color.Lerp(new Color(sr.color.r, sr.color.g, sr.color.b, 1), new Color(sr.color.r, sr.color.g, sr.color.b, 0.2f), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
            if (progress >= 1)
            {
                StartCoroutine(FadeIn(i, 0.01f, 1f));
            }
        }
    }
}
