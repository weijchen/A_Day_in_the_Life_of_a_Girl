using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDrag : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 lastMousePosition = Vector3.zero;
    
    private bool isMouseDown = false;
    //bool isReachWall = false;

    private void Start()
    {
        
    }
    void Update()
    {

        if (isMouseDown)
        {
            if (lastMousePosition != Vector3.zero)
            {
                Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
                this.transform.position += new Vector3 (offset.x,0);
            }
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
    }

    public void OnMouseDown()
    {
        isMouseDown = true;
    }

    public void OnMouseUp()
    {
        isMouseDown = false;
        lastMousePosition = Vector3.zero;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.position);
        if (collision.gameObject.name == "Wall")
        {
            isMouseDown = false;
            lastMousePosition = Vector3.zero;
        }
    }
}
