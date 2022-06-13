using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalGround : MonoBehaviour
{
    float rayDis = 0.25f;       //レイの長さ
    public bool isGoal;         //ゴールしたか

    // Start is called before the first frame update
    void Start()
    {
        isGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayPos = transform.position;        //レイの位置
        Ray ray = new Ray(rayPos, Vector3.up);      //レイの位置と向きのセット
        RaycastHit hit;
        Debug.DrawRay(rayPos, Vector3.up * rayDis, Color.white);    //レイの可視化

        //レイに当たった時の処理
        if(Physics.Raycast(ray,out hit,rayDis))
        {
            isGoal = true;
        }
    }
}
