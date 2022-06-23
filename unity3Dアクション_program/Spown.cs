using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spown : MonoBehaviour
{
    //出現させる敵
    [SerializeField]
    GameObject[] enemy = null;
    //次に出現させるまでの時間
    [SerializeField]
    float NextTime = 0.0f;
    //出現させる敵の最大数
    [SerializeField]
    int MaxEnemy = 0;
    //出現している敵の数
    private int NumEnemy;
    //待ち時間
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        NumEnemy = 0;
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //出現させる最大数を超えた場合
        //何もしない
        if(NumEnemy>=MaxEnemy)
        {
            return;
        }

        //経過時間を足す
        elapsedTime += Time.deltaTime;

        //経過時間が経ったら
        if(elapsedTime>NextTime)
        {
            elapsedTime = 0.0f;

            AppearEnemy();
        }
    }

    //敵の出現
    void AppearEnemy()
    {
        //出現させる敵をランダムで選ぶ
        var randomValue = Random.Range(0, enemy.Length);
        //敵の向きをランダムで決める
        var randomRotationY = Random.value * 360f;

        GameObject.Instantiate(enemy[randomValue], transform.position, Quaternion.Euler(0.0f, randomRotationY, 0.0f));

        NumEnemy++;
        elapsedTime = 0.0f;
    }
}
