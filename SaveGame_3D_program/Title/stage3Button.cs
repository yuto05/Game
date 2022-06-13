using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class stage3Button : MonoBehaviour
{
    public GameObject notplayPanel;

    //フェード
    public GameObject fadePanel;
    fadeScripts fade;

    StageGoal.StageIndex myData = new StageGoal.StageIndex();

    int stageIndex;

    // Start is called before the first frame update
    void Start()
    {
        //クリアしたステージ数をロードする
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
        //ステージ2以降をクリアしている場合
        if (stageIndex >= 2) 
        {
            fade.fadeOut = true;    //フェードアウト開始
            Invoke("ChangeScene", 1.0f);  //1秒後にシーン遷移
        }
        else
        {
            notplayPanel.SetActive(true);
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

    void ChangeScene()
    {
        SceneManager.LoadScene("Stage3Scene");
    }
}
