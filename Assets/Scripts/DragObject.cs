using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 lastMousePosition = Vector3.zero;
    private Vector3 originPos;
    private bool isMouseDown = false;
    bool iscollition = false;
    public string TargetName;
    private void Start()
    {
        originPos = transform.position;
    }
    void Update()
    {

        if (isMouseDown)
        {
            if (lastMousePosition != Vector3.zero)
            {
                Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
                this.transform.position += offset;
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
        if (iscollition)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = originPos;
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.name == TargetName)
        {
            iscollition = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == TargetName)
        {
            iscollition = false;
        }
    }
}
