using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{

    public List<AudioClip> currentDialog1st = new();
    public List<AudioClip> currentDialog2nd = new();
    public List<AudioClip> actualDialog = new();

    public int NumberOfSpeaker = 1;
    public int counter;

    public int NumberOfDialog = 0;
    public int NumberOfReplica = 0;
    public int NumberOfAnims = 0;

    Speech _speechScript;

    public Animator animator1st;
    public Animator animator2nd;

    public bool IsAnimationEnd = false;
    public bool IsAnimationEnd1st = false;
    public bool IsAnimationEnd2nd = false;
    public bool StartConversation = false;
    // Start is called before the first frame update
    void Start()
    {
        //GetRandomMonologue();
        animator1st = GameObject.Find("Cube").GetComponent<Animator>();
        animator2nd = GameObject.Find("Cube (1)").GetComponent<Animator>();
        NumberOfSpeaker = 1;
        counter = 0;

        GetAudioDialog();
        CurrentForming();
        ParseAudioNamesToNumberOfAnims();
        StartConversation = false;
        //StartCoroutine(Dialog());
        
    }

    public void GetAudioDialog()
    {
        NumberOfDialog = UnityEngine.Random.Range(1, 5);
        actualDialog.AddRange(Resources.LoadAll<AudioClip>($"Sound/Rep{NumberOfDialog}"));

    }

    public void GetRandomMonologue()
    {
        int randomMonologue = UnityEngine.Random.Range(1, 37);
        actualDialog.Add(Resources.Load<AudioClip>($"Sound/Random1/Replica ({randomMonologue})"));
    }


    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartConversation = true;
            //StartCoroutine(Dialog());
        }

        if (StartConversation && counter < currentDialog1st.Count)
        {
            if (NumberOfSpeaker == 1 && !IsAnimationEnd && animator1st.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Debug.Log(currentDialog1st[counter]);
                
                animator1st.SetInteger("TriggerInt", 1);
                animator2nd.SetInteger("TriggerInt", -1);
                NumberOfSpeaker = 2;
            }
            if (NumberOfSpeaker == 2 && !IsAnimationEnd && animator2nd.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Debug.Log(currentDialog2nd[counter]);
                
                animator1st.SetInteger("TriggerInt", -1);
                animator2nd.SetInteger("TriggerInt", 2);
                NumberOfSpeaker = 1;
            }
            counter++;

        }
        if (counter >= currentDialog1st.Count)
        {
            StartConversation = false;
        }

    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NumberOfSpeaker = UnityEngine.Random.Range(1, 3);
            StartConversation = true;
            counter = 0;
            if (NumberOfSpeaker == 1)
            {
                animator1st.SetInteger("TriggerInt", 1);
            }
            else
            {
                animator2nd.SetInteger("TriggerInt", 2);
            }
        }
        if (StartConversation)
        {
            if (animator1st.GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeaker == 1)
            {
                Debug.Log(actualDialog[counter]);
                animator1st.SetInteger("TriggerInt", -1);
                animator2nd.SetInteger("TriggerInt", 2);
                NumberOfSpeaker = 2;
                counter++;
            }
            if (animator2nd.GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeaker == 2)
            {
                Debug.Log(actualDialog[counter]);
                animator1st.SetInteger("TriggerInt", 1);
                animator2nd.SetInteger("TriggerInt", -1);
                NumberOfSpeaker = 1;
                counter++;
            }
            if (counter >= actualDialog.Count)
            {
                StartConversation= false;
                animator1st.SetInteger("TriggerInt", -1);
                animator2nd.SetInteger("TriggerInt", -1);
            }
        }
        
    }


    void CurrentForming()
    {

        for (int j = 0; j < actualDialog.Count; j++)
        {
            if (j % 2 == 0)
            {
                currentDialog1st.Add(actualDialog[j]);
            }
            else
            {
                currentDialog2nd.Add(actualDialog[j]);
            }

        }

    }

    private void ParseAudioNamesToNumberOfAnims()
    {
        string audioName = actualDialog[0].name.Substring(3);
        NumberOfReplica = int.Parse(audioName);
        NumberOfAnims = NumberOfReplica;

    }

    IEnumerator Dialog()
    {
        yield return null;
        /*if (StartConversation)
        {
            for (int i = 0; i < currentDialog1st.Count; i++)
            {
                Debug.Log("___NEW DOUBLE___");
                yield return new WaitForSeconds(1);
                animator1st.SetInteger("TriggerInt", 1);
                yield return new WaitWhile(() => IsAnimationEnd == false);
                animator1st.SetInteger("TriggerInt", -1);
                Debug.Log("Replica 1" + currentDialog1st[i]);
                //yield return new WaitForSeconds(3);
                animator2nd.SetInteger("TriggerInt", 2);
                yield return new WaitWhile(() => IsAnimationEnd == false);
                animator2nd.SetInteger("TriggerInt", -1);
                Debug.Log("Replica 2" + currentDialog2nd[i]);
            }
        }
        StartConversation = false;*/




        /*for (int i = 0; i < currentDialog1st.Count; i++)
        {
            if (NumberOfSpeaker == 1)
            {
                animator1st.SetInteger("TriggerInt", 1);
                animator2nd.SetInteger("TriggerInt", -1);
                yield return new WaitWhile(() => IsAnimationEnd == false);
                Debug.Log(currentDialog1st[i]);
                NumberOfSpeaker++;
            }

            if (NumberOfSpeaker == 2)
            {
                animator2nd.SetInteger("TriggerInt", 2);
                animator1st.SetInteger("TriggerInt", -1);
                yield return new WaitWhile(() => IsAnimationEnd == false);
                Debug.Log(currentDialog2nd[i]);
                NumberOfSpeaker--;
            }
            
        }
        animator1st.SetInteger("TriggerInt", -1);
        animator2nd.SetInteger("TriggerInt", -1);
        StopAllCoroutines();*/

    }
}
