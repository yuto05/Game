using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //制限時間
    public float countTime = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //countTimeにゲームを開始してからの秒数を格納
        countTime -= Time.deltaTime;

        //少数点以下桁を表示しない
        GetComponent<Text>().text = countTime.ToString("F0");

        //制限時間を過ぎた場合
        if (countTime <= 0)
        {
            countTime = 0;

            Time.timeScale = 0f;
        }

        //ゲームが終了した場合
        if(Time.timeScale == 0f)
        {
            //クリアタイムの保存
            PlayerPrefs.SetFloat("ScoreTime", countTime);
            PlayerPrefs.Save();
        }
    }
}
