using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //パーティクルが終了した場合
        if(particle.isStopped)
        {
            //パーティクルを削除
            Destroy(this.gameObject);
        }
    }
}
