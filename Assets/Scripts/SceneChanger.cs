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
            int currSceneInd = SceneManager.GetActiveScene().buildIndex;
            FadeToLevel(currSceneInd + 1);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        _animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
