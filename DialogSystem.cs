using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public int sequenceDialog;
    public int randomMonologue;
    public List<QuestionEveryday> questionEverydays = new List<QuestionEveryday>();
    public List<AnswerEveryday> answerEverydays = new List<AnswerEveryday>();
    public List<AudioClip> currentDialog1st = new List<AudioClip>();
    public List<AudioClip> currentDialog2nd = new List<AudioClip>();
    public List<FullDialog> fullDialogs = new List<FullDialog>();

    public List<List<AudioClip>> dialogAudioList = new List<List<AudioClip>>();

    public List<AudioClip> actualDialog = new List<AudioClip>();

    public bool isExistRep = false;

    // Start is called before the first frame update
    void Start()
    {
        //CreateClasses();
        //sequenceDialog = Random.Range(1, 5);
        //CurrentDialogForming(sequenceDialog);
        //StartCoroutine(Dialog());
        /*CreateFullDialog();
        sequenceDialog = UnityEngine.Random.Range(0, fullDialogs.Count);
        CurrentForming(sequenceDialog);
        StartCoroutine(Dialog());*/
        //FindAndGetRepsByRandomNumber();
        //GetAudioDialog();
        GetRandomMonologue();
    }

    private void FindAndGetRepsByRandomNumber()
    {
        sequenceDialog = UnityEngine.Random.Range(1, 5);
        int i = 1;
        isExistRep = true;
        while (isExistRep)
        {
            if (Resources.Load<AudioClip>($"Sound/Rep{sequenceDialog}_{i}") == null)
            {
                isExistRep = false;
            }
            else
            {
                actualDialog.Add(Resources.Load<AudioClip>($"Sound/Rep{sequenceDialog}_{i}"));
            }
            i++;
        }
    }

    public void GetAudioDialog()
    {
        sequenceDialog = UnityEngine.Random.Range(1, 5);
        actualDialog.AddRange(Resources.LoadAll<AudioClip>($"Sound/Rep{sequenceDialog}"));

    }

    public void GetRandomMonologue()
    {
        randomMonologue = UnityEngine.Random.Range(1, 37);
        actualDialog.Add(Resources.Load<AudioClip>($"Sound/Random1/Replica ({randomMonologue})"));
    }


    // Update is called once per frame
    void Update()
    {

    }

    void AudioClipsCreate()
    {
        for (int i = 0; i < fullDialogs.Count; i++)
        {
            dialogAudioList.Add(new List<AudioClip>());
            for (int j = 0; j < fullDialogs[i].DialogStrings.Count; j++)
            {
                dialogAudioList[i].Add(Resources.Load<AudioClip>($"Sound/{fullDialogs[i].DialogStrings[j]}"));
            }
        }
    }

    /*void CreateFullDialog()
    {
        List<string> stringFl = new List<string>
        {
            "������! ��� ����?",
            "�����������! �� ���������. � ��� ����",
            "�� ������ �������. ����� ������� ���������.",
            "��� �� ������ �������! �����, ��� ���� ����, �������� ����� ������, ���������� �� ���������."
        };
        fullDialogs.Add(new FullDialog(stringFl));
        stringFl.Clear();

        stringFl = new List<string>
        {
            "������ ����! ��� �������� �����?",
            "�� ���������, ������� ������������."
        };
        fullDialogs.Add(new FullDialog(stringFl));
        stringFl.Clear();

        stringFl = new List<string>
        {
            "��� ������? ����� �����, ������ ����?",
            "��, �����. ������, ��� ���� �����������."
        };
        fullDialogs.Add(new FullDialog(stringFl));
        stringFl.Clear();

        stringFl = new List<string>
        {
            "��� ���������� � ����� �����?",
            "�� ������ ���������, ����� ������ �������.",
            "��? ��� ���������? ������ �����! ��� ������� �������, ������� ������ ���� �� ������.",
            "��� �������, ��� �� ������������ � �����������. ��������� �� ���� �������� ����������� " +
            "� ��� �� ����� �� �������, � ��-�� ���� ���������� ��� �����, ��� �������� ������ ����������.",
            "������, ������ �� ����� �������� � �������� � ���.",
            "������� �������."
        };
        fullDialogs.Add(new FullDialog(stringFl));
        stringFl.Clear();
    }*/

    void CreateFullDialog()
    {
        List<string> stringFl = new List<string>
        {
            "Rep1_1",
            "Rep1_2",
            "Rep1_3",
            "Rep1_4"
        };

        fullDialogs.Add(new FullDialog(stringFl));
        stringFl.Clear();


        stringFl = new List<string>
        {
            "Rep2_1",
            "Rep2_2"
        };

        fullDialogs.Add(new FullDialog(stringFl));
        stringFl.Clear();


        stringFl = new List<string>
        {
            "Rep3_1",
            "Rep3_2"
        };

        fullDialogs.Add(new FullDialog(stringFl));
        stringFl.Clear();


        stringFl = new List<string>
        {
            "Rep4_1",
            "Rep4_2",
            "Rep4_3",
            "Rep4_4",
            "Rep4_5",
            "Rep4_6"
        };

        fullDialogs.Add(new FullDialog(stringFl));
        stringFl.Clear();
        AudioClipsCreate();
    }

    void CurrentForming(int seqNum)
    {

        for (int j = 0; j < dialogAudioList[seqNum].Count; j++)
        {
            if (j % 2 == 0)
            {
                currentDialog1st.Add(dialogAudioList[seqNum].ElementAt(j));
            }
            else
            {
                currentDialog2nd.Add(dialogAudioList[seqNum].ElementAt(j));
            }

        }

    }

    void CreateClasses()
    {
        QuestionEveryday _1QRep = new QuestionEveryday("������! ��� ����?", 1, 1);
        AnswerEveryday _1ARep = new AnswerEveryday("�����������! �� ���������. � ��� ����?", 2, 1);
        QuestionEveryday _2QRep = new QuestionEveryday("�� ������ �������. ����� ������� ���������.", 1, 1);
        AnswerEveryday _2ARep = new AnswerEveryday("��� �� ������ �������! �����, ��� ���� ����, �������� ����� ������, ���������� �� ���������.", 2, 1);

        QuestionEveryday _3QRep = new QuestionEveryday("������ ����! ��� �������� �����?", 1, 2);
        AnswerEveryday _3ARep = new AnswerEveryday("�� ���������, ������� ������������.", 2, 2);

        QuestionEveryday _4QRep = new QuestionEveryday("��� ������? ����� �����, ������ ����?", 1, 3);
        AnswerEveryday _4ARep = new AnswerEveryday("��, �����. ������, ��� ���� �����������.", 2, 3);

        QuestionEveryday _5QRep = new QuestionEveryday("��� ���������� � ����� �����?", 1, 4);
        AnswerEveryday _5ARep = new AnswerEveryday("�� ������ ���������, ����� ������ �������.", 2, 4);
        QuestionEveryday _6QRep = new QuestionEveryday("��? ��� ���������? ������ �����! ��� ������� �������, ������� ������ ���� �� ������.", 1, 4);
        AnswerEveryday _6ARep = new AnswerEveryday("��� �������, ��� �� ������������ � �����������. ��������� �� ���� �������� ����������� " +
            "� ��� �� ����� �� �������, � ��-�� ���� ���������� ��� �����, ��� �������� ������ ����������.", 2, 4);
        QuestionEveryday _7QRep = new QuestionEveryday("������, ������ �� ����� �������� � �������� � ���.", 1, 4);
        AnswerEveryday _7ARep = new AnswerEveryday("������� �������.", 2, 4);

        questionEverydays.Add(_1QRep);
        questionEverydays.Add(_2QRep);
        questionEverydays.Add(_3QRep);
        questionEverydays.Add(_4QRep);
        questionEverydays.Add(_5QRep);
        questionEverydays.Add(_6QRep);
        questionEverydays.Add(_7QRep);

        answerEverydays.Add(_1ARep);
        answerEverydays.Add(_2ARep);
        answerEverydays.Add(_3ARep);
        answerEverydays.Add(_4ARep);
        answerEverydays.Add(_5ARep);
        answerEverydays.Add(_6ARep);
        answerEverydays.Add(_7ARep);

    }

    /*void CurrentDialogForming(int seqNum)
    {
        for (int i = 0; i < questionEverydays.Count; i++)
        {
            if (questionEverydays[i].SequenceNumber == seqNum)
            {
                currentDialog1st.Add(questionEverydays[i].QuestionString);
            }
        }
        for (int i = 0; i < answerEverydays.Count; i++)
        {
            if (answerEverydays[i].SequenceNumber == seqNum)
            {
                currentDialog2nd.Add(answerEverydays[i].AnswerString);
            }
        }
    }*/

    IEnumerator Dialog()
    {
        yield return null;
        for (int i = 0; i < currentDialog1st.Count; i++)
        {
            Debug.Log(currentDialog1st[i]);
            yield return new WaitForSeconds(0.5f);
            Debug.Log(currentDialog2nd[i]);
            yield return new WaitForSeconds(0.5f);
        }
        currentDialog1st.Clear();
        currentDialog2nd.Clear();
        Debug.Log(dialogAudioList.Count + "Count");
        Debug.Log(dialogAudioList[0].Count);
        Debug.Log(dialogAudioList[1].Count);
        Debug.Log(dialogAudioList[2].Count);
        Debug.Log(dialogAudioList[3].Count);
        /*Debug.Log(fullDialogs.Count);
        Debug.Log(fullDialogs[0].DialogStrings.Count);
        Debug.Log(fullDialogs[1].DialogStrings.Count);
        Debug.Log(fullDialogs[2].DialogStrings.Count);
        Debug.Log(fullDialogs[3].DialogStrings.Count);*/
    }
}
