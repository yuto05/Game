using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalManager : MonoBehaviour
{
    //フェード
    public GameObject fadePanel;
    fadeScripts fade;
    //クリア
    public GameObject clearUI;
    float count;
    //ゴール
    public StageGoal goal;
    //ポーズ
    public GameObject pauseUI;
    public EndPause end;

    // Start is called before the first frame update
    void Start()
    {
        //フェードの情報を取得
        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        //終了処理
        if(Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }

        //クリア後の処理
        if(goal.isGoal)
        {
            //経過時間の加算
            count += Time.unscaledDeltaTime;
            clearUI.SetActive(true);        //clearUIのアクティブ化

            //2.5秒経過
            if(count>=2.5f)
            {
                //フェードアウト開始
                fade.fadeOut = true;
            }
            //4.5秒経過
            if(count>=4.5f)
            {
                //シーン遷移
                ChangeScene();
            }
        }
        //クリアしていない時の処理
        if(!goal.isGoal)
        {
            //ポーズのキーを押された場合
            if(Input.GetKeyDown(KeyCode.P))
            {
                //アクティブの切り替え
                pauseUI.SetActive(!pauseUI.activeSelf);
            }
            
            //ポーズUIがアクティブ化されてる場合
            if(pauseUI.activeSelf)
            {
                //一時停止
                Time.timeScale = 0;
            }
            //非アクティブ化
            if(!pauseUI.activeSelf)
            {
                //再開
                Time.timeScale = 1;
            }

            //エンドが押された場合
            if (end.isEnd)
            {
                fade.fadeOut = true;        //フェードアウト
                //カウント
                count += Time.unscaledDeltaTime;

                //1秒後
                if (count >= 1.0f)
                {
                    //シーン遷移
                    ChangeScene();
                }
            }
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
