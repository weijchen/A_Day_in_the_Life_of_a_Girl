using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchOffEffect : MonoBehaviour
{
    [SerializeField] private int revealAmount = 800;
    [SerializeField] private GameObject maskPrefab;
    [SerializeField] private GameObject nextArrow;
    
    private bool isPressed = false;

    private void Start()
    {
        nextArrow.SetActive(false);
    }

    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 5.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (isPressed)
        {
            GameObject maskSprite = Instantiate(maskPrefab, mousePos, Quaternion.identity);
            maskSprite.transform.parent = gameObject.transform;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            if (gameObject.transform.childCount >= revealAmount)
            {
                Reveal();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }
    }

    void Reveal()
    {
        nextArrow.SetActive(true);
        Destroy(gameObject);
    }
}
