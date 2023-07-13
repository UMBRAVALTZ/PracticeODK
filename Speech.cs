using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour
{

    public AudioSource audioSource;
    public DialogSystem _dialogueSystemScript;
    Animator anim;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _dialogueSystemScript = GameObject.Find("FPS").GetComponent<DialogSystem>();
        anim = GetComponent<Animator>();
    }

    public void PlaySound()
    {
        audioSource.Play();
        Debug.Log("StartSound");
    }

    public void StopSound()
    {
        audioSource.Stop();
        anim.SetInteger("TriggerInt", 0);

        /*_dialogueSystemScript._animators[i][0].SetInteger("TriggerInt", 0);

        _dialogueSystemScript._animators[i][1].SetInteger("TriggerInt", 0);
        */


        Debug.Log("StopSound");
    }

}
