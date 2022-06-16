using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    //フェード
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
        /*if(fade.fadeOut)
        {
            Invoke("ChangeScene", 1.0f);
        }*/
    }

    //ボタンが押下された時の処理
    public void OnClickStartButton()
    {
        fade.fadeOut = true;
        Invoke("ChangeScene", 1.0f);        //1秒後にシーン遷移開始

        source.PlayOneShot(clickClip);
    }

    //シーン遷移
    void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
