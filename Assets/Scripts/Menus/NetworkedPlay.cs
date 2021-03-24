
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkedPlay : MonoBehaviour
{
    public Text textDisplay;

    private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int maxPlayersPerRoom = 2;

    private void Awake()
    {
    }

    private void Start()
    {
        FindOpponent();

    }
    public void SetCharacter()
    {
    }
    public void FindOpponent()
    {
        
    }

   
}

