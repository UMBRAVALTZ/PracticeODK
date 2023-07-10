using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour
{

    public AudioSource audioSource;
    public DialogSystem _dialogueSystemScript;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _dialogueSystemScript = GameObject.Find("FPS").GetComponent<DialogSystem>();
    }

    public void PlaySound()
    {
        audioSource.Play();
        _dialogueSystemScript.IsAnimationEnd = false;
        //Debug.Log("---IsAnimatedEnd = false---");
    }

    public void StopSound()
    {
        audioSource.Stop();
        _dialogueSystemScript.IsAnimationEnd = true;
        //Debug.Log("---IsAnimatedEnd = true---");
    }

}
