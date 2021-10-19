using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Transform[] Images;
    public int currentindex;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToNext()
    {
        Images[currentindex].gameObject.SetActive(false);
        AddIndex();
        Images[currentindex].gameObject.SetActive(true);
    }

    public void FadeToNext()
    {
        StartFadeOut(Images[currentindex].gameObject);
        AddIndex();
        Images[currentindex].gameObject.SetActive(true);
        StartFadeIn(Images[currentindex].gameObject);
    }

    public void AddIndex()
    {
        currentindex++;
        if (currentindex == Images.Length)
        {
            Debug.Log("warning");
        }
    }






    public void StartFadeIn(GameObject i)
    {
        StartCoroutine(FadeIn(i, 0.01f, 1f));
    }

    public void StartFadeOut(GameObject i)
    {
        StartCoroutine(FadeOut(i, 0.01f, 1f));
        
    }
    IEnumerator FadeOut(GameObject i, float smoothness, float duration)
    {

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        SpriteRenderer sr = i.GetComponent<SpriteRenderer>();
        while (progress < 1)
        {

            sr.color = Color.Lerp(new Color(sr.color.r, sr.color.g, sr.color.b, 1), new Color(sr.color.r, sr.color.g, sr.color.b, 0), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
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

            sr.color = Color.Lerp(new Color(sr.color.r, sr.color.g, sr.color.b, 0), new Color(sr.color.r, sr.color.g, sr.color.b, 1), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

    }
}
