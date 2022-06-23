using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject target;              //追いかける対象(プレイヤー)
    Vector3 tarPos;                         //追いかける対象(プレイヤー)の位置
    float distance;                         //追いかける対象との距離
    private NavMeshAgent agent;             //NavMeshAgentを取得する変数
    [SerializeField]
    float trackingRange = 3f;               //targetを追いかけ始める距離
    [SerializeField]
    float quitRange = 5f;                   //追いかけるのをやめる距離
    [SerializeField]
    float attackRange = 1f;
    [SerializeField]
    bool Attacking = false;
    [SerializeField]
    bool tracking = false;                  //追いかけているか
    Transform points;
    private Animator anim;                  //アニメーション
    PlayerHP pHp;

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgentを取得する
        agent = GetComponent<NavMeshAgent>();

        //autoBrakingを無効にすると目標地点の間を継続的に移動
        agent.autoBraking = false;

        //目的地の設定
        GotoNextPoint();

        //追いかける対象(プレイヤー)の情報を取得
        target = GameObject.Find("unitychan");      //ヒエラルキー内のunitychanの情報
        pHp = GameObject.Find("unitychan").GetComponent<PlayerHP>();

        //アニメーターの情報を取得
        anim = GetComponent<Animator>();
    }

    //次の目標地点を設定
    void GotoNextPoint()
    {
        //ランダムで目標地点の値を出す
        var RandDisnation = Random.insideUnitCircle * 30;

        //agentが現在設定された目標地点に行くようにする
        agent.destination = new Vector3(RandDisnation.x, 0, RandDisnation.y);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //ターゲットとの距離を測る
        tarPos = target.transform.position;
        distance = Vector3.Distance(this.transform.localPosition, tarPos);

        if (!Attacking)
        {
            if (tracking)
            {
                if (distance < attackRange)
                {
                    Attacking = true;
                    StartCoroutine(AttackTimer());
                    return;
                }
                //追跡時.quitRangeより離れたら中止
                else if (distance > quitRange)
                {
                    tracking = false;
                }

                agent.speed = 3;
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);

                //ターゲットを目標にする
                agent.destination = tarPos;
            }
            else
            {
                //ターゲットがtrackingRangeより近づいたら追跡開始
                if (distance < trackingRange)
                {
                    tracking = true;
                }

                agent.speed = 1;
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);

                //現時点に近づいたら次の目標を設定
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    Invoke("GotoNextPoint", 0f); //GotoNextPoint();
                }
            }
        }
        /*else
        {
            StartCoroutine(AttackTimer());
        }*/
    }

    IEnumerator AttackTimer()
    {
        if(Attacking)
        {
            agent.speed = 0;
            anim.SetTrigger("Attack");
            pHp.currentHP = pHp.currentHP - 1;
            yield return new WaitForSeconds(1);
            
            Attacking = false;
        }

        yield return null;
    }

    /*void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="Player")
        {
            //StartCoroutine(AttackTimer());
        }
    }*/
}
