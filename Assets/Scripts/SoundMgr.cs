using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundMgr : MonoBehaviour
{
    public List<AudioClip> audios;
    public List<AudioClip> dialogues;
    public static SoundMgr Instance = null;
    
    
    AudioSource audioSource;
    public int dialogueIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(int clipIndex)
    {
        audioSource.PlayOneShot(audios[clipIndex]);
        
    }

    public void PlayDialogue()
    {
        if (dialogueIndex == dialogues.Count)
        {
            Debug.LogWarning("dialogue index exceeds range!");
            return;
        }
        audioSource.PlayOneShot(dialogues[dialogueIndex]);
        dialogueIndex++;
    }

    public void PlayBGM(int clipIndex)
    {
        audioSource.clip=audios[clipIndex];
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    

   
}
