using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutlialButton : MonoBehaviour
{
    //フェードの情報を格納する
    public GameObject fadePanel;
    fadeScripts fade;
    //オーディオ
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clickClip;
    // Start is called before the first frame update
    void Start()
    {
        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        fade.fadeOut = true;            //フェードアウトを開始する
        Invoke("ChangeScene", 1.0f);    //1秒後にシーン遷移を行う
        source.PlayOneShot(clickClip);
    }

    //シーン遷移
    void ChangeScene()
    {
        SceneManager.LoadScene("TutlialScene");
    }
}
