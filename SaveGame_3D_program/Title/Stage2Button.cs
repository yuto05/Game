using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Stage2Button : MonoBehaviour
{
    StageGoal.StageIndex myData = new StageGoal.StageIndex();
    public GameObject notPlay;

    //フェード
    fadeScripts fade;
    public GameObject fadePanel;

    int stageIndex;

    // Start is called before the first frame update
    void Start()
    {
        //クリアしたステージ数をロード
        LoadStageIndex();
        //フェードの情報を格納
        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        //ステージ1以降をクリアした場合
        if(stageIndex>=1)
        {
            fade.fadeOut = true;    //フェードアウト開始
            Invoke("ChangeScene", 1.0f);    //1秒後にシーン遷移
        }
        else
        {
            notPlay.SetActive(true);
        }
    }

    //ステージをいくつクリアしたかロード
    public void LoadStageIndex()
    {
        string datastr;
        StreamReader reader;

        if(File.Exists(Application.dataPath + "/SaveStageIndex.json"))
        {
            //徐々にデータを読み込む
            reader = new StreamReader(Application.dataPath + "/SaveStageIndex.json");
            datastr = reader.ReadToEnd();
            reader.Close();

            //Json形式から変換
            myData = JsonUtility.FromJson<StageGoal.StageIndex>(datastr);
        }

        stageIndex = myData.SaveIndex;
    }

    //シーン遷移処理
    void ChangeScene()
    {
        SceneManager.LoadScene("Stage2Scene");
    } 
}
