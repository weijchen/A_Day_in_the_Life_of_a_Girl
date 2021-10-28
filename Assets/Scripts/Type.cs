using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public GameObject Dialog1;
    public GameObject Dialog2;
    public GameObject Dialog3;
    public GameObject SendMessage1;
    public GameObject ImageManager;
    AudioSource audio;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeToNextDialog());
        audio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeToNextDialog()
    {
        yield return new WaitForSeconds(2f);
        audio.Play();
        animator.SetBool("Typing", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Typing", false);
        audio.Stop();
        yield return new WaitForSeconds(5f);
        Dialog1.SetActive(false);
        Dialog2.SetActive(true);
        animator.SetBool("Typing", true);
        audio.Play();
        yield return new WaitForSeconds(2f);
        animator.SetBool("Typing", false);
        audio.Stop();
        yield return new WaitForSeconds(5f);
        Dialog2.SetActive(false);
        Dialog3.SetActive(true);
        animator.SetBool("Typing", true);
        audio.Play();
        yield return new WaitForSeconds(5f);
        audio.Stop();
        Dialog3.SetActive(false);
        SendMessage1.SetActive(true);
        ImageManager.GetComponent<ImageManager>().FadeToNext(4f);
    }
}

