using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

//ノーマルステージのゴールの処理
public class StageGoal : MonoBehaviour
{
    float rayDis = 0.25f;       //レイの長さ
    
    //ステージ解放
    public class StageIndex
    {
        public int SaveIndex;   //クリアしたステージ数
    }

    int Index;
    public bool isGoal = false;

    //セーブロード
    string filePath;            //ファイル名
    StageIndex myData = new StageIndex();
    // Start is called before the first frame update
    void Start()
    {
        //ファイル名
        filePath = Application.dataPath + "/SaveStageIndex.json";
        //クリアしたステージのインデックスを読み込む
        LoadStageIndex();
    }

    // Update is called once per frame
    void Update()
    {
        //レイ
        Vector3 rayPos = transform.position;        //レイの位置
        Ray ray = new Ray(rayPos, Vector3.up);      //レイの位置と向きのセット
        RaycastHit hit;                             //ヒットしたものの情報の格納
        Debug.DrawRay(rayPos, Vector3.up * rayDis, Color.blue); //レイの可視化

        //ステージインデックスのセット
        int stageIndex = SceneManager.GetActiveScene().buildIndex;

        //ゴール
        if(isGoal)
        {
            //停止
            Time.timeScale = 0;
        }

        //レイにヒットした時の処理
        if(Physics.Raycast(ray,out hit,rayDis))
        {
            //今プレイしているステージ以降のステージをクリアしたことあるか
            if(Index<=stageIndex)
            {
                SaveStageGoal();
            }
            isGoal = true;
        }
    }

    //ステージをクリアしたかセーブする処理
    public void SaveStageGoal()
    {
        StreamWriter writer;
        myData.SaveIndex = SceneManager.GetActiveScene().buildIndex;

        string jsonstr = JsonUtility.ToJson(myData);        //Json形式へ変換

        //徐々にデータを書き込む
        writer = new StreamWriter(filePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    //今プレイしているステージ以降の
    //ステージをクリアしたことがあるかロードする処理
    public void LoadStageIndex()
    {
        string datastr;
        StreamReader reader;

        //ファイルがあるか確認
        //なければ生成
        if(File.Exists(filePath))
        {
            //徐々にデータを読み込む
            reader = new StreamReader(filePath);
            datastr = reader.ReadToEnd();
            reader.Close();

            myData = JsonUtility.FromJson<StageIndex>(datastr); //Json形式から変換
        }

        Index = myData.SaveIndex;
    }

    //シーン遷移処理
    void ChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
