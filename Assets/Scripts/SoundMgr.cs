using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundMgr : MonoBehaviour
{
    [Header("Audio List")]
    [SerializeField] private List<AudioClip> bgmList;
    [SerializeField] private List<AudioClip> sfxList;
    [SerializeField] private List<AudioClip> dialoguesList;
    [SerializeField] private int dialogueIndex = 0;

    [Header("FadeIn Configs")] 
    [SerializeField] private float smoothness = 0.5f;
    [SerializeField] private float increase = 0.05f;
    [SerializeField] private float maxVolume = 1.0f;

    public static SoundMgr Instance = null;

    private bool startBgm = true;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
        PlayBGM(0);
        VolumeFadeIn();
    }

    public void PlaySoundFromClick(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(sfxList[index]);
    }

    public void PlayDialogue()
    {
        if (dialogueIndex == dialoguesList.Count)
        {
            Debug.LogWarning("dialogue index exceeds range!");
            return;
        }
        audioSource.PlayOneShot(dialoguesList[dialogueIndex]);
        dialogueIndex++;
    }

    public void PlayBGM(int clipIndex)
    {
        audioSource.clip = bgmList[clipIndex];
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    private void VolumeFadeIn()
    {
        StartCoroutine(StartVolumeFadeIn(smoothness, increase, maxVolume));
    }

    IEnumerator StartVolumeFadeIn(float smoothness, float inc, float maxVol)
    {
        while (audioSource.volume < maxVol)
        {
            audioSource.volume += inc;
            yield return new WaitForSeconds(smoothness);
        }
    }
}
