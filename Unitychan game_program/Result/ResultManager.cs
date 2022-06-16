using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    //フェードするのに必要なもの
    fadeScripts fade;
    public GameObject fadePanel;
    bool transition = false;

    //オーディオに必要なもの
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clickClip;

    // Start is called before the first frame update
    void Start()
    {
        //フェードパネルの情報を呼び出す
        fade = fadePanel.GetComponent<fadeScripts>();
        //ゲームシーンで0にしたタイムスケールを1に戻す
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //スペースが押下された場合
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!transition)
            {
                fade.fadeOut = true;
                Invoke("ChangeScene", 1.5f);        //1.5秒後にシーン遷移を開始する
                source.PlayOneShot(clickClip);
                transition = true;
            }
        }

        //Escキーが押下された場合
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }
    }

    //終了処理
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    //シーン遷移
    void ChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
