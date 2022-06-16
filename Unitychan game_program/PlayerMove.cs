using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;                       //Rigidbodyの取得
    [SerializeField]float walk = 20.0f; //移動速度
    float jumpForce = 250.0f;           //ジャンプ量
    Vector3 playerpos;                  //プレイヤーの位置
    /*[SerializeField]
    bool onGround = true;*/               //地面の上にいるかの判定
    int key = 0;                        //入力キーの取得
    private Collider WeponCol;          //武器のコライダー

    //アニメーションするのに必要
    string state;                       //今の状態
    string oldstate;                    //前の状態
    private Animator anim;              //animatorの情報
    public FloatingJoystick Joystick;   //JoyStick

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();     //Rigidbodyの取得
        anim = GetComponent<Animator>();    //animatorの取得
        playerpos = transform.position;     //プレイヤーの位置の取得
        WeponCol = GameObject.Find("Sword_OH").GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Statecheck();
        //ChangeAnimation();
    }

    //状態のチェック
    /*void Statecheck()
    {
        //地面と接触している状態
        if(onGround)
        {
            //キーが入力された場合
            if (key != 0)
            {
                state = "RUN";
            }
            //それ以外
            else
            {
                state = "IDLE";
            }
        }
        //接触していない状態
        else
        {
            state = "JUMP";
        }
    }*/

    //プレイヤーの移動
    void Move()
    {
        //左右移動
        float x = Joystick.Horizontal;
        float kx = Input.GetAxis("Horizontal");
        if (x != 0 || kx != 0)
        {
            key = 1;
        }

        //前後移動
        float y = Joystick.Vertical;
        float ky = Input.GetAxis("Vertical");
        if (y != 0 || ky != 0)
        {
            key = 1;
        }

        //攻撃処理
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            //攻撃アニメーション再生
            anim.SetTrigger("Attack");

            WeponCol.enabled = true;
            Invoke("ColliderReset", 0.3f);
        }

        //攻撃中の処理
        //これを入れることによって
        //攻撃中はこれより下の処理は行われない
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack_Combo"))
        {
            return;
        }

        //攻撃中は移動することができない
        //joystick操作の場合
        if (x != 0 || y != 0)
        {
            rb.MovePosition(transform.position + new Vector3(x * Time.deltaTime * walk, 0, y * Time.deltaTime * walk));
            anim.SetBool("isRun", true);
        }
        //キーボード操作の場合
        else if (kx != 0 || ky != 0)
        {
            rb.MovePosition(transform.position + new Vector3(kx * Time.deltaTime * walk, 0, ky * Time.deltaTime * walk));
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        //rb.MovePosition(transform.position + new Vector3(x * Time.deltaTime * walk, 0, y * Time.deltaTime * walk));

        Vector3 direction = transform.position - playerpos;

        //プレイヤーを移動する方向に向かせる
        if (direction.magnitude >= 0.01f && key == 1)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        else
        {
            key = 0;
        }

        //playerの位置更新
        playerpos = transform.position;
    }

    //状態の遷移
    /*void ChangeAnimation()
    {
        if(oldstate!=state)
        {
            switch(state)
            {
                case "JUMP":
                    anim.SetBool("isJump", true);
                    anim.SetBool("isRun", false);
                    anim.SetBool("isWait", false);
                    break;
                case "RUN":
                    anim.SetBool("isJump", false);
                    anim.SetBool("isRun", true);
                    anim.SetBool("isWait", false);
                    break;
                default:
                    anim.SetBool("isJump", false);
                    anim.SetBool("isRun", false);
                    anim.SetBool("isWait", true);
                    break;
            }
            oldstate = state;
        }
    }*/

    //当たり判定の処理
    void OnCollisionEnter(Collision col)
    {
        /*if(!onGround)
        {
            onGround = true;
        }*/
    }

    private void ColliderReset()
    {
        WeponCol.enabled = false;
    }
}
