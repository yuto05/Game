using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class RankingScript : MonoBehaviour
{
    //ランキング変数
    public class Ranking
    {
        public float first = 0f;     //1位
        public float second = 0f;    //2位
        public float third = 0f;     //3位
    }

    //ランキングのテキスト
    public GameObject firstText;    //1位のテキスト
    public GameObject secondText;   //2位
    public GameObject thirdText;    //3位
    public GameObject clearText;

    //フェード
    fadeScripts fade;
    public GameObject fadePanel;

    bool isLoop = true;             //ランキングの処理を通ったか判定
    float scoreData;                //スコアの保存

    Ranking myData = new Ranking();
    ClearTime.timer myData1 = new ClearTime.timer();

    //オーディオ
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        //クリア後0にしたまま
        //1にしないとフェードもシーン遷移もできない
        Time.timeScale = 1;

        //クリアタイムとランキングのデータの読み込み
        LoadTimeData();
        LoadClearTime();

        if(myData.first<=0)
        {
            SaveRanking();
        }

        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        //ランキングの処理
        if(isLoop)
        {
            //1位にデータがなかった場合
            if (myData.first <= 0)
            {
                myData.first = myData1.counter;
            }
            //あった場合
            else
            {
                //2位にデータがなかった場合
                if (myData.second <= 0)
                {
                    //クリアタイムが1位より速かった場合
                    if(myData.first>myData1.counter)
                    {
                        myData.second = myData.first;
                        myData.first = myData1.counter;
                    }
                    //クリアタイムが1位より遅かった場合
                    else
                    {
                        myData.second = myData1.counter;
                    }
                }
                //あった場合
                else
                {
                    //3位にデータがなかった場合
                    if (myData.third <= 0)
                    {
                        //クリアタイムが2位より速かった場合
                        if(myData.second>myData1.counter)
                        {
                            //クリアタイムが1位より速かった場合
                            if(myData.first>myData1.counter)
                            {
                                myData.third = myData.second;
                                myData.second = myData.first;
                                myData.first = myData1.counter;
                            }
                            //遅かった場合
                            else
                            {
                                myData.third = myData.second;
                                myData.second = myData1.counter;
                            }
                        }
                        //遅かった場合
                        else
                        {
                            myData.third = myData1.counter;
                        }
                    }
                    //あった場合
                    else
                    {
                        //クリアタイムが3位より速い場合
                        if(myData.third>myData1.counter)
                        {
                            myData.third = myData1.counter;

                            //2位よりも速い場合
                            if(myData.second>myData.third)
                            {
                                scoreData = myData.third;
                                myData.third = myData.second;
                                myData.second = scoreData;

                                //1位よりも速い場合
                                if(myData.first>myData.second)
                                {
                                    scoreData = myData.second;
                                    myData.second = myData.first;
                                    myData.first = scoreData;
                                }
                            }
                        }
                    }
                }
            }

            //ランキングをセーブする
            SaveRanking();
            //上記の処理が1回だけ行われるようfalseに変える
            isLoop = false;

            Debug.Log("ランキング");
        }
        
        if (!isLoop)
        {
            //いずれかのキーを押されたらシーン遷移
            if (Input.anyKeyDown)
            {
                fade.fadeOut = true;
                Invoke("ChangeScene", 1.0f);
                source.PlayOneShot(clip);

                Debug.Log("押下");
            }
        }

        //テキストの表示
        firstText.GetComponent<Text>().text = "1st:" + ((int)(myData.first / 60)).ToString("00") + ":" + ((int)(myData.first % 60)).ToString("00");
        secondText.GetComponent<Text>().text = "2nd:" + ((int)(myData.second / 60)).ToString("00") + ":" + ((int)(myData.second % 60)).ToString("00");
        thirdText.GetComponent<Text>().text = "3rd:" + ((int)(myData.third / 60)).ToString("00") + ":" + ((int)(myData.third % 60)).ToString("00");
        clearText.GetComponent<Text>().text = ((int)(myData1.counter / 60)).ToString("00") + ":" + ((int)(myData1.counter % 60)).ToString("00");
    }

    public void LoadTimeData()
    {
        //ファイルの有無の確認
        if(File.Exists(Application.dataPath + "/RankingData.json"))
        {
            StreamReader reader;

            //ランキングデータを読み込む
            reader = new StreamReader(Application.dataPath + "/RankingData.json");
            string datastr = reader.ReadToEnd();
            reader.Close();

            myData = JsonUtility.FromJson<Ranking>(datastr);

            Debug.Log("ok");
        }
        
    }

    public void LoadClearTime()
    {
        StreamReader reader;

        //クリアタイムを読み込む
        reader = new StreamReader(Application.dataPath + "/SaveClearTime.json");
        string str = reader.ReadToEnd();
        reader.Close();

        myData1 = JsonUtility.FromJson<ClearTime.timer>(str);

        Debug.Log("y");
    }

    public void SaveRanking()
    {
        StreamWriter writer;

        string str = JsonUtility.ToJson(myData);

        writer = new StreamWriter(Application.dataPath + "/RankingData.json", false);
        writer.Write(str);
        writer.Flush();
        writer.Close();
    }

    //シーン遷移
    void ChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
