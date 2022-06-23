using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    //パーティクルの情報を格納
    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        //パーティクルの情報を取得
        particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //パーティクルが終了したか
        if(particle.isStopped)
        {
            Destroy(this.gameObject);   //パーティクル用ゲームオブジェクトを削除
        }
    }
}
