using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Longmode : MonoBehaviour
{
    //ボタンのオブジェクト
    public GameObject longSelect;
    public GameObject back;
    public GameObject normalmode;
    public GameObject notPlayPanel;

    int stageIndex;     //初クリアしたステージ数

    StageGoal.StageIndex myData = new StageGoal.StageIndex();

    // Start is called before the first frame update
    void Start()
    {
        //クリアしたステージのインデックスを読み取る
        LoadStageIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタンがクリックされた処理
    public void OnClickStartButton()
    {
        //ステージ3までクリアしたか
        if(stageIndex>=3)
        {
            Invoke("ChangeButton", 0.25f);
        }
        //クリアしていなかった場合
        else
        {
            notPlayPanel.SetActive(true);
        }
    }

    //ステージをいくつクリアしたか読み込む
    public void LoadStageIndex()
    {
        string datastr;
        StreamReader reader;

        //ファイルの有無の確認
        if(File.Exists(Application.dataPath + "/SaveStageIndex.json"))
        {
            //徐々にデータを読み込む
            reader = new StreamReader(Application.dataPath + "/SaveStageIndex.json");
            datastr = reader.ReadToEnd();
            reader.Close();

            myData = JsonUtility.FromJson<StageGoal.StageIndex>(datastr);   //Json形式から変換
        }
        
        stageIndex = myData.SaveIndex;
    }

    //ボタンのアクティブの切り替え
    void ChangeButton()
    {
        longSelect.SetActive(true);
        back.SetActive(true);
        normalmode.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
