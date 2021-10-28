using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBubbles : MonoBehaviour
{
    [SerializeField] private List<Transform> bubbles;

    void Start()
    {
        StartFadeOut();
    }

    private void StartFadeOut()
    {
        foreach (Transform bubble in bubbles)
        {
            StartCoroutine(FadeOut(bubble, 0.01f, 4.0f));
        }        
    }

    IEnumerator FadeOut(Transform i, float smoothness, float duration)
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        SpriteRenderer sr = i.GetComponent<SpriteRenderer>();
        while (progress <= 1)
        {
            sr.color = Color.Lerp(new Color(sr.color.r, sr.color.g, sr.color.b, 1), new Color(sr.color.r, sr.color.g, sr.color.b, 0), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        i.gameObject.SetActive(false);
    }
}
