using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScoreScript : MonoBehaviour
{
    int highScore;          //ハイスコア
    int score;              //今回のスコア
    float highScoreTime;    //最高タイム
    float scoreTime;        //今回のタイム
    public GameObject highScoreGUI;
    public GameObject scoreGUI;
    public GameObject timeGUI;
    public GameObject highTimeGUI;
    // Start is called before the first frame update
    void Start()
    {
        //キーに保存されている値を呼び出す
        //保存されていなかった場合0を返す
        highScore = PlayerPrefs.GetInt("HighScore", 0);             //ハイスコア
        score = PlayerPrefs.GetInt("Score", 0);                     //今回のスコア
        highScoreTime = PlayerPrefs.GetFloat("HighScoreTime", 0);   //ハイスコア時のクリアタイム
        scoreTime = PlayerPrefs.GetFloat("ScoreTime", 0);           //今回のクリアタイム
    }

    // Update is called once per frame
    void Update()
    {
        //ハイスコア更新
        //ハイスコアの時よりもクリアタイムが速かった場合
        if(highScoreTime<=scoreTime)
        {
            if(highScore<=score)
            {
                highScoreTime = scoreTime;
                highScore = score;
            }
        }

        //スコア表示
        highScoreGUI.GetComponent<Text>().text = highScore.ToString();
        scoreGUI.GetComponent<Text>().text = score.ToString();
        highTimeGUI.GetComponent<Text>().text = highScoreTime.ToString("F2");
        timeGUI.GetComponent<Text>().text = scoreTime.ToString("F2");

        //スコア更新
        //キーに保存(上書き)する
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetFloat("HighScoreTime", highScoreTime);
        ResetKey();

        //今回書き換えられたキーのセーブを行う
        PlayerPrefs.Save();
    }

    //スコアリセット
    void ResetKey()
    {
        //今回のスコア、クリアタイムが保存されているキーの削除を行う
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("ScoreTime");
    }
}
