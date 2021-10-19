using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class ClickEvent : UnityEvent<string>
{

} 
public class Click : MonoBehaviour
{
    // Start is called before the first frame update
    public ClickEvent clickEvent;
    void Start()
    {
        if (clickEvent == null)
        {
            clickEvent = new ClickEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null &&hit.collider.gameObject==this.gameObject)
            {
                clickEvent.Invoke("11");
                //Debug.Log(hit.collider.gameObject.name);
            }

        }
    }

    public void Test()
    {
        Debug.Log("Testing");
    }
}
