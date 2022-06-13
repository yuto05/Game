using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalJump : MonoBehaviour
{

    public NormalPlayer player;         //ノーマルモードのプレイヤーのスクリプト
    public float jumpTime;              //ジャンプボタン(スペース)の押下時間
    public float jumpForce = 5.0f;      //ジャンプ力
    public float r, g, b, a;            //RGBA値
    float old;                          //RGBAの初期値を保存する変数

    //オーディオ
    [SerializeField]
    AudioSource source;                 //ソース
    [SerializeField]
    AudioClip clip;                     //クリップ

    // Start is called before the first frame update
    void Start()
    {
        //値の設定
        r = 1;
        g = 1;
        b = 1;
        a = 1;
        old = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //地面にいる場合
        if(player.isGround)
        {
            //押下時
            if (Input.GetButton("Jump"))
            {
                jumpTime += Time.deltaTime * 5f;      //押下時間の測定
                
                //5以上になった場合
                if (jumpTime >= 5f)
                {
                    jumpTime = 5f;      //5に戻す
                }
                else
                {
                    //色の変更
                    g -= Time.deltaTime;    //グリーンの値変更
                    b -= Time.deltaTime;    //ブルーの値変更
                }
            }

            //離した瞬間
            if (Input.GetButtonUp("Jump"))
            {
                //playerのスクリプトに変数を返す
                player.JumpButton(jumpTime * jumpForce, jumpTime);

                source.PlayOneShot(clip);

                jumpTime = 0;       //押下時間リセット
                player.isGround = false;
            }
        }
        //地面から離れた場合
        if (player.isGround == false)
        {
            jumpTime = 0;           //押下時間リセット

            //色を戻す
            g = old;    //グリーン値を戻す
            b = old;    //ブルー値を戻す
        }

        //色の更新
        GetComponent<Renderer>().material.color = new Color(r, g, b, a);
    }
}
