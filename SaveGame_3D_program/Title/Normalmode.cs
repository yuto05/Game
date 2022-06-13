using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Normalmode : MonoBehaviour
{
    //ボタンのオブジェクト
    public GameObject stageselect;
    public GameObject LongButton;
    public GameObject titleBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        //0.25秒後にボタンの切り替え
        Invoke("ChangeButton", 0.25f);
    }

    //ボタンのアクティブの切り替え
    void ChangeButton()
    {
        stageselect.SetActive(true);
        titleBack.SetActive(true);
        LongButton.SetActive(false);
        gameObject.SetActive(false);
    }
}
