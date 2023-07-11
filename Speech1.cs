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
        Debug.Log("StartSound");
    }

    public void StopSound()
    {
        audioSource.Stop();
        _dialogueSystemScript.animator1st.SetInteger("TriggerInt", 0);
        _dialogueSystemScript.animator2nd.SetInteger("TriggerInt", 0);
        Debug.Log("StopSound");
    }

}
