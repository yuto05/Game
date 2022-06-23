using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;          //プレイヤーの情報格納
    private Vector3 offset;             //相対格納取得

    // Start is called before the first frame update
    void Start()
    {
        //playerの情報取得
        //this.player = GameObject.Find("unitychan");         //ヒエラルキー内のunitychanの情報

        //MainCameraとplayerとの相対距離を求める
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //新しい値を代入
        transform.position = player.transform.position + offset;
    }
}
