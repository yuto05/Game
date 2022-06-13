using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Button : MonoBehaviour
{
    //フェード
    fadeScripts fade;
    public GameObject fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        //フェードの情報を格納
        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        fade.fadeOut = true;            //フェードアウト開始
        Invoke("ChangeScene", 1.0f);    //1秒後にシーン遷移
    }

    //シーン遷移処理
    void ChangeScene()
    {
        SceneManager.LoadScene("Stage1Scene");
    }
}
