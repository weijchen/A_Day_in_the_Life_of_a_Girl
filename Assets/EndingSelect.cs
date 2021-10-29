using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSelect : MonoBehaviour
{
    public void ChgHE()
    {
        SceneManager.LoadScene(7);
    }
    
    public void ChgBE()
    {
        SceneManager.LoadScene(8);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(1);
    }
}
