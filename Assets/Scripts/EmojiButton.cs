using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmojiButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MynextMessage;
    public GameObject girlfriendmessage;
    public GameObject NextButton;
    public GameObject NearButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendMessage()
    {
        StartCoroutine(ChangeToNextMessage());
    }
    IEnumerator ChangeToNextMessage()
    {
        GetComponent <Click>().enabled = false;
        NearButton.GetComponent<Click>().enabled = false;
        MynextMessage.SetActive(true);
        
        if (girlfriendmessage != null)
        {
            yield return new WaitForSeconds(3f);
            girlfriendmessage.SetActive(true);
            
        }
        if (SceneManager.GetActiveScene().name != "Scene2")
        {
            MynextMessage.SetActive(false);
        }
        transform.parent.gameObject.SetActive(false);
        NextButton.SetActive(true);
       

    }

    public void Dialog()
    {

 
    
    }

    IEnumerator ChangeToNextDialog()
    {
        GetComponent<Click>().enabled = false;
        NearButton.GetComponent<Click>().enabled = false;
        MynextMessage.SetActive(true);

        if (girlfriendmessage != null)
        {
            yield return new WaitForSeconds(3f);
            girlfriendmessage.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
        MynextMessage.SetActive(false);
        NextButton.SetActive(true);


    }
}
