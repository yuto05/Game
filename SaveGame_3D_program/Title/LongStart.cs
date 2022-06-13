using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LongStart : MonoBehaviour
{
    //フェード
    public GameObject FadePanel;
    fadeScripts fade;

    LongLoad.LoadData myData = new LongLoad.LoadData();

    // Start is called before the first frame update
    void Start()
    {
        //フェードの情報を格納
        fade = FadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        //ロードボタンが押されたか情報をセーブ
        SaveLoadData();

        fade.fadeOut = true;
        Invoke("ChangeScene", 1.0f);    //1秒後にシーン遷移
    }

    //ロードボタンが押されたかセーブ
    public void SaveLoadData()
    {
        StreamWriter writer;
        myData.isLoad = false;

        //Json形式へ変換
        string jsonstr = JsonUtility.ToJson(myData);

        //徐々にデータを書き込む
        writer = new StreamWriter(Application.dataPath + "/ClickLoad.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    //シーン遷移
    void ChangeScene()
    {
        SceneManager.LoadScene("LongModeScene");
    }
}
