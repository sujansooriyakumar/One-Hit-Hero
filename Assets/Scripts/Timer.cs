using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;

public class Timer : MonoBehaviourPunCallbacks
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
            BaseCharacter[] players = GameObject.FindObjectsOfType<BaseCharacter>();
            foreach (BaseCharacter c in players)
            {
                //c.DisconnectFromServer();
            }
            
            time = 99;
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom(true);

    }
}
