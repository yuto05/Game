using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ClearTime : MonoBehaviour
{
    public GoalGround goal;         //ゴールのスクリプト

    //タイマー
    public class timer
    {
        public float counter = 0f;  //タイマーカウンター(秒)
    }

    public float saveTimer;         //時間をセーブ

    timer myData = new timer();
    LongLoad.LoadData myData1 = new LongLoad.LoadData();
    GameManager.PlayerData myData2 = new GameManager.PlayerData();

    // Start is called before the first frame update
    void Start()
    {
        //情報を読み込む
        Load();

        //ロードボタンが押されている
        if(myData1.isLoad)
        {
            //セーブされていたタイムを代入
            myData.counter = myData2.SaveTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ゴールしていない場合
        if(!goal.isGoal)
        {
            //時間を進める
            //ポーズ中も時間を測定する
            myData.counter += Time.unscaledDeltaTime;       //カウントアップ
            saveTimer = myData.counter;
        }
        //ゴールした場合
        if(goal.isGoal)
        {
            //クリアした時間をセーブする
            SaveGoalTime();

            myData2.isSave = false;
        }

        //時間をテキストに表示する
        GetComponent<Text>().text = ((int)(myData.counter/60)).ToString("00")+":"+((int)(myData.counter%60)).ToString("00");
    }

    //クリアした時間をセーブ
    public void SaveGoalTime()
    {
        StreamWriter writer;
        StreamWriter writer1;

        string jsonstr = JsonUtility.ToJson(myData);
        string str = JsonUtility.ToJson(myData2);

        //クリアタイムを書き込む
        writer = new StreamWriter(Application.dataPath + "/SaveClearTime.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();

        //クリアしたのでセーブされているデータを扱えないように書き込む
        writer1 = new StreamWriter(Application.dataPath + "/SaveData.json", false);
        writer1.Write(str);
        writer1.Flush();
        writer1.Close();
    }

    //ロード
    public void Load()
    {
        StreamReader reader;
        StreamReader reader1;

        //ファイルの有無を確認
        if(File.Exists(Application.dataPath + "/ClickLoad.json"))
        {
            //ロードボタンが押されたか読み込む
            reader = new StreamReader(Application.dataPath + "/ClickLoad.json");
            string str = reader.ReadToEnd();
            reader.Close();

            //Json形式から変換
            myData1 = JsonUtility.FromJson<LongLoad.LoadData>(str);
        }

        //ファイルの有無を確認
        if(File.Exists(Application.dataPath + "/SaveData.json"))
        {
            //セーブされている時間を読み込む
            reader1 = new StreamReader(Application.dataPath + "/SaveData.json");
            string str1 = reader1.ReadToEnd();
            reader1.Close();

            //Json形式から変換
            myData2 = JsonUtility.FromJson<GameManager.PlayerData>(str1);
        }
    }
}
