using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private ImageManager _imageManager;
    private int finalImageInd;
    private Animator _animator;
    private int levelToLoad;

    private void Start()
    {
        _imageManager = FindObjectOfType<ImageManager>();
        finalImageInd = _imageManager.transform.childCount - 1;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_imageManager.GetCurrentIndex() == finalImageInd && Input.GetMouseButtonDown(0))
        {
            if (SceneManager.GetActiveScene().name != "Scene5")
            {
                int currSceneInd = SceneManager.GetActiveScene().buildIndex;
                FadeToLevel(currSceneInd + 1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                SoundMgr.Instance.PlayBGM(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                // SceneManager.LoadScene("Intro");
                SoundMgr.Instance.PlayBGM(2);
            }


        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        _animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (levelToLoad == 5)
        {
            SoundMgr.Instance.PlayBGM(1);
        }
        else if (levelToLoad == 6)
        {
            SoundMgr.Instance.PlayBGM(2);
        }
        SceneManager.LoadScene(levelToLoad);
    }
}
