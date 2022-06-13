using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Stage4Button : MonoBehaviour
{
    public GameObject FadePanel;
    fadeScripts fade;

    public GameObject noPlayPanel;

    int Index;

    StageGoal.StageIndex myData = new StageGoal.StageIndex();

    // Start is called before the first frame update
    void Start()
    {
        //クリアしたステージのインデックスをロードする
        LoadStageIndex();
        //フェードの情報を格納
        fade = FadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        //ステージ3以降のステージをクリアしたか
        if(Index>=3)
        {
            fade.fadeOut = true;
            Invoke("ChangeScene", 1.0f);
        }
        else
        {
            noPlayPanel.SetActive(true);
        }
    }

    public void LoadStageIndex()
    {
        StreamReader reader;
        string str;

        if(File.Exists(Application.dataPath+"/SaveStageIndex.json"))
        {
            reader = new StreamReader(Application.dataPath + "/SaveStageIndex.json");
            str = reader.ReadToEnd();
            reader.Close();

            myData = JsonUtility.FromJson<StageGoal.StageIndex>(str);
        }

        Index = myData.SaveIndex;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Stage4Scene");
    }
}
