using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LongLoad : MonoBehaviour
{
    //ロードボタンが押されたか判断する変数
    [System.Serializable]
    public class LoadData
    {
        public bool isLoad;
    }

    //フェード
    public GameObject FadePanel;
    fadeScripts fade;

    public GameObject noPlayButton;
    bool isSave;        //セーブされているか

    LoadData myData = new LoadData();
    GameManager.PlayerData myData2 = new GameManager.PlayerData();

    // Start is called before the first frame update
    void Start()
    {
        //データがセーブされているかロードする
        Loadsave();

        fade = FadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        //セーブされているか
        if(isSave)
        {
            //ロードボタンが押されたという情報をセーブ
            SaveLoadData();

            fade.fadeOut = true;
            Invoke("ChangeScene", 1.0f);
        }
        //セーブされていない
        else if(!isSave)
        {
            noPlayButton.SetActive(true);
        }
    }

    //ロードボタンが押されたかセーブ
    public void SaveLoadData()
    {
        StreamWriter writer;
        myData.isLoad = true;

        string jsonstr = JsonUtility.ToJson(myData);    //Json形式へ変換

        //徐々にデータを書き込む
        writer = new StreamWriter(Application.dataPath + "/ClickLoad.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    //データがセーブされているかロードする
    public void Loadsave()
    {
        StreamReader reader;
        string str;

        //ファイルの有無を確認
        if(File.Exists(Application.dataPath + "/SaveData.json"))
        {
            //徐々にデータを読み込む
            reader = new StreamReader(Application.dataPath + "/SaveData.json");
            str = reader.ReadToEnd();
            reader.Close();

            myData2 = JsonUtility.FromJson<GameManager.PlayerData>(str); //Json形式から変換
        }

        isSave = myData2.isSave;
    }

    //シーン遷移
    void ChangeScene()
    {
        SceneManager.LoadScene("LongModeScene");    //シーン遷移
    }
}
