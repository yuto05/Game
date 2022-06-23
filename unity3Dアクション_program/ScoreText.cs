using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text scoreText;          //スコアのテキスト
    public int Score = 0;          //残りの敵の数
    int MaxScore = 75;     //敵の最大数
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        //今回のスコアをセット
        PlayerPrefs.GetInt("Score", 0);
    }

    //スコア加算
    public void AddScore()
    {
        Score++;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Score.ToString();

        //今回のスコアをセット
        PlayerPrefs.SetInt("Score", Score);
        //キーと値を保存
        PlayerPrefs.Save();

        //最大スコアになったとき
        if (Score==MaxScore)
        {
            Time.timeScale = 0;
        }
    }
}
