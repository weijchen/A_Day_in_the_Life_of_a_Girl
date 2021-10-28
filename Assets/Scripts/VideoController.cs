using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeToIntro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeToIntro()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Intro");
    }
}
