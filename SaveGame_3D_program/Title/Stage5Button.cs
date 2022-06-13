using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Stage5Button : MonoBehaviour
{
    public GameObject fadePanel;
    fadeScripts fade;

    public GameObject noPlay;

    int Index;

    StageGoal.StageIndex myData = new StageGoal.StageIndex();

    // Start is called before the first frame update
    void Start()
    {
        fade = fadePanel.GetComponent<fadeScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        //ステージ4以降のステージをクリアしたか
        if(Index>=4)
        {
            fade.fadeOut = true;
            Invoke("ChangeScene", 1.0f);
        }
        else
        {
            noPlay.SetActive(true);
        }
    }

    void LoadStageIndex()
    {
        StreamReader read;
        string str;

        if (File.Exists(Application.dataPath + "/SaveStageIndex.json"))
        {
            read = new StreamReader(Application.dataPath + "SaveStageIndex.json");
            str = read.ReadToEnd();
            read.Close();

            myData = JsonUtility.FromJson<StageGoal.StageIndex>(str);
        }

        Index = myData.SaveIndex;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Stage5Scene");
    }
}
