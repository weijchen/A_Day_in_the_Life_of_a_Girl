using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talking : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject girlfriendbubble;
    public GameObject Button1;
    public GameObject mom;
    public GameObject Dad;
    void Start()
    {
        StartCoroutine(FadeIn(girlfriendbubble, 0.01f, 2f));
        StartCoroutine(FadeIn(mom, 0.01f, 2f));
        StartCoroutine(FadeIn(Dad, 0.01f, 2f));
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
                Button1.SetActive(true);
            }
        }
    }
}
