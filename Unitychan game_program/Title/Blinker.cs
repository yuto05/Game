using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{
    public float speed = 1.0f;      //点滅速度(0に近い値ほど点滅は遅くなる)
    public Text text;                      //点滅させるテキスト
    private float time;             //点滅時間

    // Start is called before the first frame update
    void Start()
    {
        //text = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time);

        return color;
    }
}
