using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    int counter = 0;                //移動してる時間
    public float move = 0.01f;      //移動量   マイナスの場合左に動きプラスの場合右に動く
    public int MaxCounter = 100;    //移動してる最大時間

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //移動変数
        Vector3 p = new Vector3(move, 0, 0);

        transform.Translate(p);         //床を移動させる
        counter++;                      //経過時間

        //最大時間になった場合
        if (counter == MaxCounter)
        {
            counter = 0;        //カウンターを0に戻す
            move *= -1;         //移動量を反転させる
        }
    }
}
