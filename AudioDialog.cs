using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDialog : MonoBehaviour
{
    public List<AudioClip> clipList;
    public List<AudioSource> sourceList;
    public List<GameObject> gameObjects;
    public Material redMat;
    public Material whiteMat;
    public List<MeshRenderer> renderers;

    // Start is called before the first frame update
    void Start()
    {
        sourceList.Add(gameObjects[0].GetComponent<AudioSource>());
        sourceList.Add(gameObjects[1].GetComponent<AudioSource>());
        clipList.Add(Resources.Load<AudioClip>("Sound/di1"));
        clipList.Add(Resources.Load<AudioClip>("Sound/di2"));
        redMat = Resources.Load<Material>("Materials/Red");
        whiteMat = Resources.Load<Material>("Materials/White");
        renderers.Add(gameObjects[0].GetComponentInParent<MeshRenderer>());
        renderers.Add(gameObjects[1].GetComponentInParent<MeshRenderer>());
        sourceList[0].clip = clipList[0];
        sourceList[1].clip = clipList[1];
        StartCoroutine(Audio());
        //sourceList[0].Play();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Audio()
    {
        yield return new WaitForSeconds(3);
        sourceList[0].Play();
        renderers[0].material = redMat;
        yield return new WaitWhile(() => sourceList[0].isPlaying);
        renderers[0].material = whiteMat;
        yield return new WaitForSeconds(0.5f);
        sourceList[1].Play();
        renderers[1].material = redMat;
        yield return new WaitWhile(() => sourceList[1].isPlaying);
        renderers[1].material = whiteMat;
    }
}
