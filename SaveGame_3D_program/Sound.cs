using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    //オーディオ
    [SerializeField]
    AudioSource source;     //ソース
    [SerializeField]
    AudioClip clip;         //クリップ
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayStart()
    {
        source.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
