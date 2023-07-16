using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogSystem1 : MonoBehaviour
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


    [SerializeField] private GameObject _cubePrefab;

    [SerializeField] private List<List<List<AudioClip>>> kekw = new();
    [SerializeField] private List<List<AudioClip>> _actualDialogList = new();
    [SerializeField] private List<int> _numberOfSpeakerList = new();
    [SerializeField] private List<bool> _startConversationList = new();
    [SerializeField] private List<List<List<AudioClip>>> _currentDialogueList = new();
    [SerializeField] private List<List<Animator>> _animators = new();
    [SerializeField] private List<List<AudioSource>> _audioSources = new();
    [SerializeField] private List<GameObject> _cubeObjects = new();
    [SerializeField] private List<List<GameObject>> _pairCubeObjects = new();
    [SerializeField] private List<int> _numberOfAnimations = new();

    public int numberOfSpeech;
    public List<Coroutine> cor = new();

    private int _indexOfElement;


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





    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateAllCubes();
            Debug.Log("SPACE IS PRESSED!!!");
            Debug.Log("PairCubes " + _pairCubeObjects.Count);
            //FillAllLists();
            //GetAudioDialog();
            //CurrentForming();
            //ParseAudioNamesToNumberOfAnims();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //StartCoroutine(StartDialogue());

            if (_indexOfElement >= 0)
            {
                StartCoroutine(Dialogue(_indexOfElement));
                //������������ ������� ������ ��� ������ � ����������� �������
                //������������ ���������� ����
            }



        }
        AddPairsByDistance();
        DeletePairsByDistance();
    }
    private void AddPairsByDistance()
    {
        int randomSpeaker;

        for (int i = 0; i < _cubeObjects.Count; i++)
        {
            for (int j = 0; j < _cubeObjects.Count; j++)
            {
                if (i != j && (Vector3.Distance(_cubeObjects[i].transform.position, _cubeObjects[j].transform.position)) <= 3f
                    && (!_cubeObjects[i].GetComponentInChildren<Speech>().IsAdded && !_cubeObjects[j].GetComponentInChildren<Speech>().IsAdded))
                {
                    List<GameObject> list = new();
                    randomSpeaker = UnityEngine.Random.Range(1, 3);
                    Debug.Log("Distance to Add");
                    _cubeObjects[i].GetComponentInChildren<Speech>().IsAdded = true;
                    _cubeObjects[j].GetComponentInChildren<Speech>().IsAdded = true;
                    if (randomSpeaker == 1)
                    {
                        _cubeObjects[i].GetComponentInChildren<Speech>().IsTalk = true;
                        list.Add(_cubeObjects[i]);
                        list.Add(_cubeObjects[j]);
                    }
                    else
                    {
                        _cubeObjects[j].GetComponentInChildren<Speech>().IsTalk = true;
                        list.Add(_cubeObjects[j]);
                        list.Add(_cubeObjects[i]);
                    }
                    
                    _pairCubeObjects.Add(list);
                    Debug.Log("PairCubes " + _pairCubeObjects.Count);
                    _indexOfElement = _pairCubeObjects.IndexOf(list);
                    FillAllLists(_indexOfElement);
                    GetAudioDialog();
                    CurrentForming(_indexOfElement);
                    ParseAudioNamesToNumberOfAnims(_indexOfElement);

                }
            }
        }
    }

    private void DeletePairsByDistance()
    {
        for (int i = 0; i < _pairCubeObjects.Count; i++)
        {

            if ((Vector3.Distance(_pairCubeObjects[i][0].transform.position, _pairCubeObjects[i][1].transform.position)) > 3f)
            {
                Debug.Log("Distance to Delete");
                _pairCubeObjects[i][0].GetComponentInChildren<Speech>().IsAdded = false;
                _pairCubeObjects[i][1].GetComponentInChildren<Speech>().IsAdded = false;
                _pairCubeObjects[i][0].GetComponentInChildren<Speech>().IsTalk = false;
                _pairCubeObjects[i][1].GetComponentInChildren<Speech>().IsTalk = false;
                ClearAllLists(i);
                DeleteAudioDialogue(i);
                DeleteCurrentDialogue(i);
                DeleteNumberOfAnims(i);
                _pairCubeObjects.RemoveAt(i);
                _indexOfElement--;
                Debug.Log("PairCubes " + _pairCubeObjects.Count);
            }

        }
    }
    private void CreateAllCubes()
    {
        float xCord = 5f;
        GameObject temp;
        for (int i = 0; i < 5; i++)
        {
            temp = Instantiate(_cubePrefab, new Vector3(xCord, 1f, 0f), Quaternion.identity);
            temp.name = "Cube" + i;
            _cubeObjects.Add(temp);
            xCord += 5f;
        }
        xCord = 5f;
        for (int i = 0; i < 5; i++)
        {
            temp = Instantiate(_cubePrefab, new Vector3(xCord, 1f, 5f), Quaternion.Euler(0, 180, 0));
            temp.name = "Cube" + (i + 5);
            _cubeObjects.Add(temp);
            xCord += 5f;
        }
    }

    private void FillAllLists(int i)
    {
        _audioSources.Add(new List<AudioSource> { _pairCubeObjects[i][0].GetComponentInChildren<AudioSource>(), _pairCubeObjects[i][1].GetComponentInChildren<AudioSource>() });
        _animators.Add(new List<Animator> { _pairCubeObjects[i][0].GetComponentInChildren<Animator>(), _pairCubeObjects[i][1].GetComponentInChildren<Animator>() });
        _startConversationList.Add(UnityEngine.Random.Range(1, 3) == 1);
        _numberOfSpeakerList.Add(UnityEngine.Random.Range(2, 3));
    }

    private void ClearAllLists(int i)
    {
        _audioSources.RemoveAt(i);
        _animators.RemoveAt(i);
        _startConversationList.RemoveAt(i);
        _numberOfSpeakerList.RemoveAt(i);
    }

    private void GetAudioDialog()
    {
        Debug.Log("AudioDialogueAdd");
        List<AudioClip> temp;
        int numOfDialogue;
        temp = new();
        numOfDialogue = UnityEngine.Random.Range(1, 5);
        temp.AddRange(Resources.LoadAll<AudioClip>($"Sound/Rep{numOfDialogue}"));
        _actualDialogList.Add(temp);

        Debug.Log("AudioDialogue " + _actualDialogList.Count);
    }

    private void DeleteAudioDialogue(int i)
    {
        Debug.Log("AudioDialogueDelete");
        _actualDialogList.RemoveAt(i);
        Debug.Log("AudioDialogue " + _actualDialogList.Count);
    }

    private void CurrentForming(int i)
    {
        Debug.Log("CurrentDialogueAdd");
        List<AudioClip> temp1;
        List<AudioClip> temp2;
        temp1 = new();
        temp2 = new();

        for (int j = 0; j < _actualDialogList[i].Count; j++)
        {
            Debug.Log("!!!");
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
            if (_numberOfSpeakerList[i] == 2)
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
        }
        if (_numberOfSpeakerList[i] == 1)
        {
            _currentDialogueList.Add(new List<List<AudioClip>> { temp1, temp2 });
        }
        if (_numberOfSpeakerList[i] == 2)
        {
            _currentDialogueList.Add(new List<List<AudioClip>> { temp2, temp1 });
        }

        Debug.Log("CurrentDialogue " + _currentDialogueList.Count);

    }

    private void DeleteCurrentDialogue(int i)
    {
        Debug.Log("CurrentDialogueDelete");
        _currentDialogueList.RemoveAt(i);
        Debug.Log("CurrentDialogue " + _currentDialogueList.Count);
    }

    private void ParseAudioNamesToNumberOfAnims(int i)
    {
        Debug.Log("NumberOfAnimAdd");
        string audioName = _currentDialogueList[i][0][0].name.Substring(3);
        int num = int.Parse(audioName);
        _numberOfAnimations.Add(num);
        Debug.Log("NumberOfAnim " + _numberOfAnimations.Count);
    }

    private void DeleteNumberOfAnims(int i)
    {
        Debug.Log("NumberOfAnimDelete");
        _numberOfAnimations.RemoveAt(i);
        Debug.Log("NumberOfAnim " + _numberOfAnimations.Count);
    }

    private void GetRandomMonologue()
    {
        int randomMonologue = UnityEngine.Random.Range(1, 37);
        actualDialog.Add(Resources.Load<AudioClip>($"Sound/Random1/Replica ({randomMonologue})"));
    }

    IEnumerator StartDialogue()
    {
        for (numberOfSpeech = 0; numberOfSpeech < _pairCubeObjects.Count; numberOfSpeech++)
        {
            yield return new WaitForSecondsRealtime(1);
            cor.Add(StartCoroutine(Dialogue(numberOfSpeech)));
            Debug.Log("cor " + numberOfSpeech + " " + cor[numberOfSpeech]);
        }

        Debug.Log("KEKW");

    }

    IEnumerator Dialogue(int i)
    {

        yield return new WaitForSecondsRealtime(1f);
        if (_startConversationList[i])
        {
            for (int j = 0; j < _currentDialogueList[i][0].Count; j++)
            {

                for (int k = 0; k < 2; k++)
                {
                    _audioSources[i][k].clip = _currentDialogueList[i][k][j];
                    _animators[i][k].SetInteger("TriggerInt", _numberOfAnimations[i]++);
                    yield return new WaitWhile(() => (_animators[i][k].GetInteger("TriggerInt") != 0));
                }

                /*if (_numberOfSpeakerList[i] == 1)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        _audioSources[i][k].clip = _currentDialogueList[i][k][j];
                        _animators[i][k].SetInteger("TriggerInt", _numberOfAnimations[i]++);
                        yield return new WaitWhile(() => (_animators[i][k].GetInteger("TriggerInt") != 0));
                    }
                }
                else if (_numberOfSpeakerList[i] == 2)
                {
                    for (int k = 1; k >= 0; k--)
                    {
                        _audioSources[i][k].clip = _currentDialogueList[i][k][j];
                        _animators[i][k].SetInteger("TriggerInt", _numberOfAnimations[i]++);
                        yield return new WaitWhile(() => (_animators[i][k].GetInteger("TriggerInt") != 0));
                    }
                }*/

            }
        }
        _startConversationList[i] = false;
        _animators[i][0].SetInteger("TriggerInt", 0);
        _animators[i][1].SetInteger("TriggerInt", 0);

    }

}
