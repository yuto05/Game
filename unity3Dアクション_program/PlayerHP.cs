using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    int MaxHP = 100;        //最大HP
    public int currentHP;          //現在のHP
    //Sliderを入れる
    public Slider slider;
    //ダメージ数
    private int Damege;
    //
    public Text HPtext;
    
    // Start is called before the first frame update
    void Start()
    {
        //Sliderを満タンにする
        slider.value = 1;
        //現在のHPと最大HPを同じに
        currentHP = MaxHP;

        Damege = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //残りHPの表示
        HPtext.text = currentHP.ToString()+"/100";
        //現在のHPからダメージを引く
        //currentHP = currentHP - Damege;

        slider.value = (float)currentHP / (float)MaxHP;

        if(currentHP<=0)
        {
            currentHP = 0;

            Time.timeScale = 0;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Enemyタグのオブジェクトに触れた場合
        if(col.gameObject.tag=="Enemy")
        {
            //現在のHPからダメージを引く
            /*currentHP = currentHP - Damege;

            //ゲージの設定
            slider.value = (float)currentHP / (float)MaxHP;*/
        }
    }
}
