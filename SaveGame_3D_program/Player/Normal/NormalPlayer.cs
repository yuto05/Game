using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalPlayer : MonoBehaviour
{
    //移動
    float x;                                //x方向の移動
    float y;                                //y方向の移動
    public float WalkSpeed = 3.0f;          //移動の速さ
    public float gravity = 9.8f;            //重力
    public bool isGround;                   //地面にいるかの判定
    bool isWall = true;                     //壁に当たったか判定
    public GameObject particle;             //ジャンプしたときに出るパーティクル

    float rayDis = 0.51f;                   //レイの長さ

    Rigidbody rb;                           //リジッドボディの情報の格納

    // Start is called before the first frame update
    void Start()
    {
        //リジッドボディの情報の格納
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //SceneIndexの取得
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        //レイの位置
        Vector3 rayPos = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        //レイの位置と向きのセット
        Ray ray1 = new Ray(rayPos, Vector3.right);
        Ray ray2 = new Ray(rayPos, Vector3.left);
        Ray ray3 = new Ray(rayPos, Vector3.up);
        //レイの可視化
        Debug.DrawRay(rayPos, Vector3.right * rayDis, Color.red);
        Debug.DrawRay(rayPos, Vector3.left * rayDis, Color.red);
        Debug.DrawRay(rayPos, Vector3.up * rayDis, Color.red);

        RaycastHit hit;     //

        //地面にいる場合の処理
        if (isGround)
        {
            //移動処理
            x = Input.GetAxis("Horizontal") * WalkSpeed;
            rb.velocity = new Vector3(x, rb.velocity.y, 0f);        //位置の更新
        }
        //地面から離れた場合
        if (!isGround)
        {
            //壁に一回も当たってない場合
            //これがないと一回壁に当たったらずっと壁に張り付き続ける
            if (isWall)
            {
                //レイが壁に当たった場合
                //跳ね返る
                if (Physics.Raycast(ray1, out hit, rayDis))
                {
                    rb.velocity = new Vector3(rb.velocity.x * -1, rb.velocity.y, 0f);        //位置の更新
                    Debug.Log("hit");
                    isWall = false;
                }
                if (Physics.Raycast(ray2, out hit, rayDis))
                {
                    rb.velocity = new Vector3(rb.velocity.x * -1, rb.velocity.y, 0f);        //位置の更新
                    Debug.Log("hit");
                    isWall = false;
                }
                if (Physics.Raycast(ray3, out hit, rayDis))
                {
                    rb.velocity = new Vector3(rb.velocity.x * -1, rb.velocity.y, 0f);        //位置の更新
                    Debug.Log("hit");
                    isWall = false;
                }
            }
        }

        //落ちてしまった場合
        if (transform.position.y <= -10)
        {
            //シーンの切り替え
            SceneManager.LoadScene(sceneIndex);
        }
    }

    //ジャンプ処理
    public void JumpButton(float jumpForce, float jumpWalk)
    {
        rb.velocity = new Vector3(x * jumpWalk * 0.5f, jumpForce, 0f);      //位置更新
        Instantiate(particle, this.transform.position, Quaternion.identity);//パーティクルを出す
        isGround = false;
    }

    //接地判定
    //接地した場合
    void OnCollisionEnter(Collision col)
    {
        if(isWall)
        {
            //地面
            if (col.gameObject.tag == "Ground")
            {
                isGround = true;
            }
            //移動床
            if(col.gameObject.tag=="moving")
            {
                isGround = true;
                //触れたオブジェと親子関係
                transform.SetParent(col.transform);
            }
        }
    }

    //離れた場合
    void OnCollisionExit(Collision col)
    {
        isWall = true;
        //地面
        if(col.gameObject.tag=="Ground")
        {
            isGround = false;
        }
        //移動床
        if(col.gameObject.tag=="moving")
        {
            isGround = false;
            //親子関係をなくす
            transform.SetParent(null);
        }
    }
}

