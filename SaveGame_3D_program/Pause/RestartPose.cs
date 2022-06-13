using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPose : MonoBehaviour
{
    public GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartClickButton()
    {
        pauseUI.SetActive(false);
    }
}
