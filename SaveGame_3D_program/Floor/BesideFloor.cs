using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BesideFloor : MonoBehaviour
{

    bool isEnter = false;           //プレイヤーが乗っているか
    public float direction = 1.0f;  //向き    -の場合左スタート、+の場合右スタート
    public float beside = 0.005f;   //速度
    public float distance = 10.0f;  //距離
    Vector3 oldPos;                 //開始時の位置

    // Start is called before the first frame update
    void Start()
    {
        //最初の位置を保存
        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //移動変数
        Vector3 p = new Vector3(beside, 0.0f, 0.0f);

        //向きが+だった場合
        if(direction>=0)
        {
            //プレイヤーが接地している
            if (isEnter)
            {
                //移動する向きを変える
                if (beside <= 0.0f)
                {
                    beside *= -1.0f;
                }

                //目的地じゃない場合
                if (transform.position.x <= oldPos.x + (distance*direction))
                {
                    //移動
                    transform.Translate(p);
                }
            }
            //接地していない
            else if (!isEnter)
            {
                //移動する向きを変える
                if (beside >= 0.0f)
                {
                    beside *= -1.0f;
                }

                //最初の位置に戻ってない場合
                if (transform.position.x >= oldPos.x)
                {
                    //移動
                    transform.Translate(p);
                }
            }
        }
        //-だった場合
        else
        {
            //接地している
            if (isEnter)
            {
                //移動する向きを変える
                if (beside >= 0.0f)
                {
                    beside *= -1.0f;
                }

                //目的地に着いてない場合
                if (transform.position.x >= oldPos.x + (distance*direction))
                {
                    //移動
                    transform.Translate(p);
                }
            }
            //接地していない
            else if (!isEnter)
            {
                //移動する向きを変える
                if (beside <= 0.0f)
                {
                    beside *= -1.0f;
                }

                //最初の位置に戻ってない場合
                if (transform.position.x <= oldPos.x)
                {
                    //移動
                    transform.Translate(p);
                }
            }
        }
    }

    //接地している処理
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            isEnter = true;
        }
    }

    //離れた処理
    void OnCollisionExit(Collision col)
    {
        isEnter = false;
    }
}
