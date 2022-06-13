using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    //スクリプト
    public PlayerMove Player;
    public ClearTime time;

    //セーブデータ
    [System.Serializable]
    public class PlayerData
    {
        public Vector3 Savepos;
        public float SaveTime;
        public bool isSave;
    }
    PlayerData myData = new PlayerData();

    //ゴール
    public GameObject goal;
    GoalGround goalground;
    //フェード
    public GameObject fadePanel;
    fadeScripts fade;

    //ポーズ中のボタン
    public SaveButton save;
    public EndPause endPause;
    //ポーズ
    public GameObject PauseUI;
    //クリア後
    public GameObject clearUI;
    float clearCount;
    
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの位置を取得
        myData.Savepos = Player.transform.position;
        //ゴールの情報を取得
        goalground = goal.GetComponent<GoalGround>();
        //フェードの情報を取得
        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        //Escキーが押された場合
        if(Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }

        //ゴールしていない
        if(!goalground.isGoal)
        {
            //ポーズの処理
            //Pを押された場合
            if (Input.GetKeyDown(KeyCode.P))
            {
                //アクティブの切り替え
                PauseUI.SetActive(!PauseUI.activeSelf);
            }
            //ポーズ中の画面停止の切り替え
            if (PauseUI.activeSelf)
            {
                //時間停止
                Time.timeScale = 0;
            }
            else if (!PauseUI.activeSelf)
            {
                //再開
                Time.timeScale = 1;
            }
        }
        
        //ゴールした場合
        if(goalground.isGoal)
        {
            //経過時間の加算
            clearCount += Time.unscaledDeltaTime;
            Time.timeScale = 0;         //画面停止

            clearUI.SetActive(true);    //UIのアクティブ

            //クリアから2.5秒経過
            if(clearCount>=2.5)
            {
                //フェードアウト開始
                fade.fadeOut = true;
            }
            //クリアから4.5秒経過
            if(clearCount>=4.5)
            {
                //シーン遷移
                ChangeScene();
            }
        }
        //タイトルに戻る
        //セーブして戻るが押された場合
        if(save.isEnd)
        {
            //セーブされた
            myData.isSave = true;
            //プレイヤーのデータをセーブする
            SavePlayerPos();
            Time.timeScale = 1;
            fade.fadeOut = true;
            Invoke("PauseChangeScene", 1.0f);
        }
        //タイトルに戻るが押された場合
        if(endPause.isEnd)
        {
            Time.timeScale = 1;
            fade.fadeOut = true;
            Invoke("PauseChangeScene", 1.0f);
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

    //クリアした場合のシーン遷移
    void ChangeScene()
    {
        SceneManager.LoadScene("RankingScene");
    }

    //ポーズで終了押されたときのシーン遷移
    void PauseChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //セーブ処理
    public void SavePlayerPos()
    {
        StreamWriter writer;

        myData.Savepos = Player.transform.position;     //プレイヤーの位置を取得
        myData.SaveTime = time.saveTimer;               //現在の経過時間を取得

        string jsonstr = JsonUtility.ToJson(myData);

        writer = new StreamWriter(Application.dataPath + "/SaveData.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();

        Debug.Log("完了");
    }
}
