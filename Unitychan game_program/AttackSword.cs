using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : MonoBehaviour
{
    public GameObject particleObj;      //パーティクルのオブジェクト
    public GameObject scoreObj;         //スコアのオブジェクト
    
    // Start is called before the first frame update
    void Start()
    {
        //scoreObjにScoreテキストの情報を取得
        //scoreObj = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Triggerに触れたとき
    void OnTriggerEnter(Collider col)
    {
        //tagがEnemyだった場合
        if(col.tag=="Enemy")
        {
            Debug.Log("敵に当たった");
            col.gameObject.SetActive(false);     //敵を使われてない状態にする
            Destroy(col.gameObject,1.5f);        //敵の削除を0.5秒後に行う

            //パーティクル出現
            Instantiate(particleObj, col.transform.position, Quaternion.identity);
            //スコア加算
            scoreObj.GetComponent<ScoreText>().AddScore();
            
        }
    }
}
