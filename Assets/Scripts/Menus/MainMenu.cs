
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string characterChoice;
    public Dropdown dropdown;
    
    private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int maxPlayersPerRoom = 2;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        characterChoice = dropdown.options[dropdown.value].text;

    }


    public void SetCharacter()
    {
        characterChoice = dropdown.options[dropdown.value].text;
    }


 

   
}
