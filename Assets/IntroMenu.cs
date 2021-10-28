using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    [SerializeField] private GameObject coverImg;
    [SerializeField] private GameObject creditImg;
    [SerializeField] private float creditTime = 3.0f;

    private bool isCreditOpenClick = false;
    private bool isCreditOpen = false;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();   
    }

    public void OpenCredit()
    {
        Debug.Log("start");
        StartCoroutine(OpenCredit(creditTime));
    }

    IEnumerator OpenCredit(float duration)
    {
        creditImg.SetActive(true);
        yield return new WaitForSeconds(duration);
        creditImg.SetActive(false);
    }
}
