using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    //タイトルで表示しているUIの情報の格納
    public GameObject spaceUI;
    public GameObject startButton;
    public GameObject tutlialButton;
    //オーディオ
    [SerializeField]
    AudioSource sourceSE;
    [SerializeField]
    AudioClip clickClip;


    // Start is called before the first frame update
    void Start()
    {
        //2つのボタンを非表示状態にする
        spaceUI.SetActive(true);
        startButton.SetActive(false);
        tutlialButton.SetActive(false);

        //ゲームシーンで0にしたタイムスケールを1に戻す
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //スペースが押下された場合
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //スペースのUIが表示されている場合
            if (spaceUI.activeSelf)
            {
                sourceSE.PlayOneShot(clickClip);
            }
            //2つのボタンを表示状態にする
            spaceUI.SetActive(false);
            startButton.SetActive(true);
            tutlialButton.SetActive(true);
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
}
