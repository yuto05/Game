using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    //ポーズしたときに表示するUI
    private GameObject pauseUI;
    //オーディオ
    public AudioClip PauseClip;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioSource BGMsource;
    //ゲーム中か判断するために必要
    public Timer timeText;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム中か
        if (timeText.countTime>0&&manager.gameStart)
        {
            if (Input.GetKeyDown("p"))
            {
                //ポーズUIのアクティブ、非アクティブの切り替え
                pauseUI.SetActive(!pauseUI.activeSelf);
                source.PlayOneShot(PauseClip);

                //UIが表示されてるときは停止
                if (pauseUI.activeSelf)
                {
                    //時間経過を停止
                    Time.timeScale = 0f;
                    BGMsource.Pause();
                }
                else
                {
                    //時間経過を開始
                    Time.timeScale = 1f;
                    BGMsource.UnPause();
                }
            }
        }
        /*if(Input.GetKeyDown("p"))
        {
            //ポーズUIのアクティブ、非アクティブの切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);
            source.PlayOneShot(PauseClip);

            //UIが表示されてるときは停止
            if (pauseUI.activeSelf)
            {
                //時間経過を停止
                Time.timeScale = 0f;
                BGMsource.Pause();
            }
            else
            {
                //時間経過を開始
                Time.timeScale = 1f;
                BGMsource.UnPause();
            }
        }*/
    }
}
