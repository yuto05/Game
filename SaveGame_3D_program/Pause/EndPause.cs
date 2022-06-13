using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPause : MonoBehaviour
{
    public bool isEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartClickButton()
    {
        isEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
