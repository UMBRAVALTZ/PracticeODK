using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogSystem : MonoBehaviour
{

    public List<AudioClip> currentDialog1st = new();
    public List<AudioClip> currentDialog2nd = new();
    public List<AudioClip> actualDialog = new();

    public int NumberOfSpeaker = 1;
    //public int counter;

    public int NumberOfDialog = 0;
    public int NumberOfReplica = 0;
    public int NumberOfAnimation = 0;

    Speech _speechScript;

    public Animator animator1st;
    public Animator animator2nd;

    public AudioSource audioSource1st;
    public AudioSource audioSource2nd;

    public bool StartConversation = false;


    private GameObject _cubePrefab;

    private List<List<List<AudioClip>>> kekw = new();
    private List<List<AudioClip>> _actualDialogList = new();
    private List<int> _numberOfSpeakerList = new();
    public List<bool> _startConversationList = new();
    private List<List<AudioClip>> _currentDialogueList = new();
    public List<List<Animator>> _animators = new();
    private List<List<AudioSource>> _audioSources = new();
    private List<GameObject> _cubeObjects = new();
    private List<List<GameObject>> _pairCubeObjects = new();
    private List<int> _numberOfAnimations = new();

    public int numberOfSpeech;
    public List<Coroutine> cor = new();


    // Start is called before the first frame update
    void Start()
    {
        //GetRandomMonologue();
        animator1st = GameObject.Find("Cubek1").GetComponentInChildren<Animator>();
        animator2nd = GameObject.Find("Cubek2").GetComponentInChildren<Animator>();
        audioSource1st = GameObject.Find("Cubek1").GetComponentInChildren<AudioSource>();
        audioSource2nd = GameObject.Find("Cubek2").GetComponentInChildren<AudioSource>();
        _cubePrefab = Resources.Load<GameObject>("Prefabs/CubeObj");
        StartConversation = false;
        //StartCoroutine(Dialog());
    }

    public void GetAudioDialog()
    {
        List<AudioClip> temp;
        for (int i = 0; i < _pairCubeObjects.Count; i++)
        {
            temp = new();
            NumberOfDialog = UnityEngine.Random.Range(1, 5);
            temp.AddRange(Resources.LoadAll<AudioClip>($"Sound/Rep{NumberOfDialog}"));
            _actualDialogList.Add(temp);
        }
    }

    public void GetRandomMonologue()
    {
        int randomMonologue = UnityEngine.Random.Range(1, 37);
        actualDialog.Add(Resources.Load<AudioClip>($"Sound/Random1/Replica ({randomMonologue})"));
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            actualDialog.Clear();
            currentDialog1st.Clear();
            currentDialog2nd.Clear();
            //NumberOfSpeaker = UnityEngine.Random.Range(1, 2);
            //StartConversation = true;


            CreateAllCubes();
            FillAllLists();
            GetAudioDialog();
            CurrentForming();
            ParseAudioNamesToNumberOfAnims();

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(StartDialogue());
            
        }
    }


    void CurrentForming()
    {

        List<AudioClip> temp1;
        List<AudioClip> temp2;
        for (int i = 0; i < _actualDialogList.Count; i++)
        {
            temp1 = new();
            temp2 = new();
            for (int j = 0; j < _actualDialogList[i].Count; j++)
            {

                if (_numberOfSpeakerList[i] == 1)
                {
                    if (j % 2 == 0)
                    {
                        temp1.Add(_actualDialogList[i].ElementAt(j));
                    }
                    else
                    {
                        temp2.Add(_actualDialogList[i].ElementAt(j));
                    }
                }
                else
                {
                    if (j % 2 == 0)
                    {
                        temp2.Add(_actualDialogList[i].ElementAt(j));
                    }
                    else
                    {
                        temp1.Add(_actualDialogList[i].ElementAt(j));
                    }
                }
            }
            if (_numberOfSpeakerList[i] == 1)
            {
                kekw.Add(new List<List<AudioClip>> { temp1, temp2 });
            }
            else
            {
                kekw.Add(new List<List<AudioClip>> { temp2, temp1 });
            }

        }

    }

    private void ParseAudioNamesToNumberOfAnims()
    {
        for (int i = 0; i < 5; i++)
        {
            string audioName = kekw[0][0][0].name.Substring(3);
            int num = int.Parse(audioName);
            _numberOfAnimations.Add(num);
        }


    }

    private void CreateAllCubes()
    {
        float xCord = 3f;
        GameObject temp;
        for (int i = 0; i < 5; i++)
        {
            temp = Instantiate(_cubePrefab, new Vector3(xCord, 1f, 3f), Quaternion.identity);
            temp.name = "Cube" + i;
            _cubeObjects.Add(temp);
            xCord += 3f;
        }
        xCord = 3f;
        for (int i = 0; i < 5; i++)
        {
            temp = Instantiate(_cubePrefab, new Vector3(xCord, 1f, 6f), Quaternion.Euler(0, 180, 0));
            temp.name = "Cube" + (i + 5);
            _cubeObjects.Add(temp);
            xCord += 3f;
        }

        for (int i = 0; i < 5; i++)
        {

            _pairCubeObjects.Add(new List<GameObject> { _cubeObjects[i], _cubeObjects[i + 5] });
        }

    }

    private void FillAllLists()
    {

        for (int i = 0; i < _pairCubeObjects.Count; i++)
        {
            _audioSources.Add(new List<AudioSource> { _cubeObjects[i].GetComponentInChildren<AudioSource>(), _cubeObjects[i + 5].GetComponentInChildren<AudioSource>() });
            _animators.Add(new List<Animator> { _cubeObjects[i].GetComponentInChildren<Animator>(), _cubeObjects[i + 5].GetComponentInChildren<Animator>() });
            _startConversationList.Add(UnityEngine.Random.Range(1, 3) == 1);
            _numberOfSpeakerList.Add(UnityEngine.Random.Range(1, 3));
        }

        for (int i = 0; i < 5; i++)
        {


        }
    }

    IEnumerator StartDialogue()
    {
        for (numberOfSpeech = 0; numberOfSpeech < _pairCubeObjects.Count; numberOfSpeech++)
        {
            yield return new WaitForSecondsRealtime(1);
            cor.Add(StartCoroutine(Dialogue(numberOfSpeech)));
            Debug.Log("cor " + numberOfSpeech + " " + cor[numberOfSpeech]);
        }
          
    }

    IEnumerator Dialogue(int i)
    {
        yield return null;
        if (_startConversationList[i])
        {
            for (int j = 0; j < kekw[i][0].Count; j++)
            {
                if (_numberOfSpeakerList[i] == 1 && _animators[i][0].GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    _animators[i][1].SetInteger("TriggerInt", 0);
                    audioSource1st.clip = kekw[i][0][j];
                    _animators[i][0].SetInteger("TriggerInt", _numberOfAnimations[i]++);
                    //yield return new WaitForSecondsRealtime(1f);
                    yield return new WaitWhile(() => (_animators[i][0].GetInteger("TriggerInt") != 0));
                    //yield return new WaitWhile(() => _animators[i][0].GetCurrentAnimatorStateInfo(0).IsName("Idle") == false);
                    _numberOfSpeakerList[i] = 2;
                    //audioSource1st.clip = null;
                }
                if (_numberOfSpeakerList[i] == 2 && _animators[i][1].GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    _animators[i][0].SetInteger("TriggerInt", 0);
                    audioSource2nd.clip = kekw[i][1][j];
                    _animators[i][1].SetInteger("TriggerInt", _numberOfAnimations[i]++);
                    //yield return new WaitForSecondsRealtime(1f);
                    yield return new WaitWhile(() => (_animators[i][1].GetInteger("TriggerInt") != 0));
                    //yield return new WaitWhile(() => _animators[i][1].GetCurrentAnimatorStateInfo(0).IsName("Idle") == false);
                    _numberOfSpeakerList[i] = 1;
                    //audioSource2nd.clip = null;
                }
            }

            _animators[i][0].SetInteger("TriggerInt", 0);
            _animators[i][1].SetInteger("TriggerInt", 0);
            _startConversationList[i] = false;
        }

    }
}
