using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public float speed;         //点滅させる速度
    public Text text;           //点滅させるテキスト
    float time;                 //点滅させる時間

    bool isPush;

    //オーディオ
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clip;

    public GameObject ModeSelect;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isPush = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Escキーが押された場合
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }

        if(!isPush)
        {
            //テキストの点滅
            text.color = GetAlphaColor(text.color);
            //いずれかのキーの入力
            if(Input.anyKeyDown)
            {
                isPush = true;
                //アクティブの切り替え
                Invoke("ChangeActive", 0.25f);
                source.PlayOneShot(clip);
            }
        }
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    //点滅処理
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time);

        return color;
    }

    //アクティブの切り替え
    void ChangeActive()
    {
        ModeSelect.SetActive(true);
        text.gameObject.SetActive(false);
    }
}
