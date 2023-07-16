using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour
{

    public AudioSource audioSource;
    public DialogSystem _dialogueSystemScript;
    Animator anim;
    public bool IsAdded = false;
    public bool IsTalk = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _dialogueSystemScript = GameObject.Find("FPS").GetComponent<DialogSystem>();
        anim = GetComponent<Animator>();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
        anim.SetInteger("TriggerInt", 0);
    }

}
