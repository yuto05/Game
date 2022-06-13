using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBack : MonoBehaviour
{

    public GameObject NormalButton;
    public GameObject LongButton;
    public GameObject stageSelect;
    public GameObject Longselect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタンが押された場合
    public void OnClickStartButton()
    {
        Invoke("ChangeButton", 0.25f);
    }

    //ボタンのアクティブの切り替え
    void ChangeButton()
    {
        stageSelect.SetActive(false);
        Longselect.SetActive(false);
        this.gameObject.SetActive(false);
        NormalButton.SetActive(true);
        LongButton.SetActive(true);
    }
}
