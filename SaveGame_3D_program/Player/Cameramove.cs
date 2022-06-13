using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour
{
    public GameObject target;       //追従する対象
    private Vector3 offset;         //対象とカメラの距離

    // Start is called before the first frame update
    void Start()
    {
        //対象とカメラの距離を図る
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //追従
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + offset.z);
    }
}
