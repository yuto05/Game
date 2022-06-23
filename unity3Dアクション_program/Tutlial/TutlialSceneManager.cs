using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutlialSceneManager : MonoBehaviour
{
    //シーンを変えるためのObj
    public GameObject sceneChangeObj;
    public GameObject fadePanel;
    fadeScripts fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        //指定のオブジェクトが非表示になった場合
        if(sceneChangeObj.activeSelf==false)
        {
            fade.fadeOut = true;
            Invoke("ChangeScene", 1.0f);    //1秒後にシーン遷移開始
        }

        //Escキーが押された場合
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
        SceneManager.LoadScene("SampleScene");
    }
}
