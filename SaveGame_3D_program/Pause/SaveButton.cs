using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveButton : MonoBehaviour
{
    public bool isEnd;

    // Start is called before the first frame update
    void Start()
    {
        isEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタンが押された場合
    public void OnClickStartButton()
    {
        isEnd = true;

        Debug.Log("押下");
    }
}
