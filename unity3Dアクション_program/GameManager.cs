using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStart = false;      //ゲームがスタートしたか
    float StartCount;                   //スタートまでのカウントダウン
    float span;                         //SEがなる間隔
    float currentTime = 0f;
    float SceneCount;                   //次のシーンに遷移するまでの時間
    public GameObject panel;            //開始前と終了後に表示する
    public Text countdownText;          //カウントダウンテキスト
    public Text endText;                //ゲーム終了を知らせるテキスト
    public GameObject timerText;
    public PlayerHP pHp;
    public ScoreText scoreTex;
    //フェードの情報を格納
    fadeScripts fade;
    public GameObject fadePanel;
    //オーディオ
    [SerializeField]
    AudioSource audioBGM;              //BGMのオーディオソース
    [SerializeField]
    AudioSource audioSE;               //SEのオーディオソース
    [SerializeField]
    AudioClip clip;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;     //カウントダウンが0になるまで停止状態にする
        StartCount = 5.0f;      //カウントダウンの初期値
        SceneCount = 0;
        span = 1f;              //間隔が1秒
        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム開始前
        if(!gameStart)
        {
            if(Time.timeScale==0)
            {
                StartCount -= Time.unscaledDeltaTime;               //スタートまでのカウントダウン
                countdownText.text = StartCount.ToString("F0");     //小数点以下桁は表示しない
                currentTime += Time.unscaledDeltaTime;              

                //一定時間ごとにSEが鳴る
                if(currentTime>=span)
                {
                    audioSE.PlayOneShot(clip);
                    currentTime = 0f;
                }
                //0になったらゲーム開始する
                if(StartCount<=0)
                {
                    gameStart = true;
                    Time.timeScale = 1;
                    audioBGM.Play();

                    //非表示にする
                    panel.SetActive(false);
                    countdownText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (timerText.GetComponent<Timer>().countTime <= 0 || pHp.currentHP <= 0 || scoreTex.Score >= 75)
            {
                //ゲーム終了後
                if (Time.timeScale == 0)
                {
                    SceneCount += Time.unscaledDeltaTime;       //シーン遷移開始するまでの時間

                    //表示する
                    panel.SetActive(true);
                    endText.gameObject.SetActive(true);
                    audioBGM.Stop();

                    //2.5秒後にフェードを開始する
                    if (SceneCount >= 2.5)
                    {
                        fade.fadeOut = true;
                    }
                    //4.5秒後にシーン遷移を行う
                    if (SceneCount >= 4.5f)
                    {
                        ChangeScene();
                    }
                }
            }
        }
        //Escキーが押された場合
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }
    }
    
    void FixedUpdate()
    {
        
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
