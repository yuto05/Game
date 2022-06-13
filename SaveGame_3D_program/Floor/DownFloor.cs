using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownFloor : MonoBehaviour
{

    bool isEnter = false;           //接地しているか
    public float down = -0.001f;    //移動速度
    Vector3 oldPos;                 //最初の位置
    public float direction = 10.0f; //距離

    // Start is called before the first frame update
    void Start()
    {
        //最初の位置の保存
        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //移動変数
        Vector3 p = new Vector3(0.0f, down, 0.0f);

        //接地している
        if(isEnter)
        {
            //向きを変える
            if(down>=0)
            {
                down *= -1;
            }

            //最初の位置よりdrection分距離が離れた場合
            if(transform.position.y>=oldPos.y-direction)
            {
                //移動
                transform.Translate(p);
            }
            
        }
        //接地していない
        else if(!isEnter)
        {
            //向きを変える
            if(down<=0)
            {
                down *= -1;
            }

            //最初の位置に戻ってない場合
            if(transform.position.y<=oldPos.y)
            {
                //移動
                transform.Translate(p);
            }
        }
    }

    //接地処理
    void OnCollisionEnter(Collision col)
    {
        //触れたオブジェのタグがPlayer
        if(col.gameObject.tag=="Player")
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
