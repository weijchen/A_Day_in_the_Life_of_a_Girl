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

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeToNextDialog());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeToNextDialog()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("Typing", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Typing", false);
        yield return new WaitForSeconds(5f);
        Dialog1.SetActive(false);
        Dialog2.SetActive(true);
        animator.SetBool("Typing", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Typing", false);
        yield return new WaitForSeconds(5f);
        Dialog2.SetActive(false);
        Dialog3.SetActive(true);
        animator.SetBool("Typing", true);
        yield return new WaitForSeconds(5f);
        Dialog3.SetActive(false);
        SendMessage1.SetActive(true);
        ImageManager.GetComponent<ImageManager>().FadeToNext(4f);
    }
}

