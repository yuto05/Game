using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeScripts : MonoBehaviour
{
    float fadeSpeed = 0.02f;        //フェードのスピード
    float red, blue, green, alfa;   //RGBAの値

    //フェードの状態
    public bool fadeIn = false;
    public bool fadeOut = false;

    Image fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        //Imageの情報を取得
        fadePanel = GetComponent<Image>();

        //RGBAの情報を取得
        red = fadePanel.color.r;
        blue = fadePanel.color.b;
        green = fadePanel.color.g;
        alfa = fadePanel.color.a;

        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn)
        {
            FadeInStart();
        }

        if (fadeOut)
        {
            FadeOutStart();
        }
    }

    void FadeInStart()
    {
        alfa -= fadeSpeed;              //徐々に下げる

        SetColor();
        if(alfa<=0)
        {
            fadeIn = false;
            fadePanel.enabled = false;  //コンポーネントをfalseにする
        }
    }

    void FadeOutStart()
    {
        fadePanel.enabled = true;       //コンポーネントをtrueにする
        alfa += fadeSpeed;              //徐々に上げる

        SetColor();
        if(alfa>=1)
        {
            fadeOut = false;
        }
    }

    //色の設定
    void SetColor()
    {
        fadePanel.color = new Color(red, green, blue, alfa);
    }
}
