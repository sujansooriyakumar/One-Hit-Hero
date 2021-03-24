using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float totalTime;
    Text text;
    int time;
    
    void Start()
    {
        time = 99;
        totalTime = 0;
        text = GetComponent<Text>();
    }

    void Update()
    {
        totalTime += Time.deltaTime;
        if(totalTime>= 1.0f)
        {
            time = time - 1;
            totalTime = 0;
        }
        text.text = "" + time;

        if(time == 0)
        {
          
        }
    }


}
