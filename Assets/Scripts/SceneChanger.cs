using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private int finalImageInd; 
    private ImageManager _imageManager;
    private Animator _animator;
    private int levelToLoad;

    private string ANIM_TRIGGER_FADEOUT = "FadeOut";

    private void Start()
    {
        _imageManager = FindObjectOfType<ImageManager>();
        finalImageInd = _imageManager.transform.childCount - 1;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_imageManager.currentindex == finalImageInd && Input.GetMouseButtonDown(0))
        {
            int currSceneInd = SceneManager.GetActiveScene().buildIndex;
            FadeToLevel(currSceneInd + 1);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        _animator.SetTrigger(ANIM_TRIGGER_FADEOUT);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
