using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMenu : MonoBehaviour
{
    GameController controller;
    private void Start()
    {
        controller = FindObjectOfType<GameController>();
        controller.isNetworked = false;

    }
    public void PlayOffline()
    {
        // set is networked to false
        // load character select
        Debug.Log("Loading offline...");
        controller.isNetworked = false;
        SceneManager.LoadScene(1);
    }

    public void PlayOnline()
    {
        // set is networked to true
        // load character select
        Debug.Log("Loading online...");
        controller.isNetworked = true;
        SceneManager.LoadScene(1);

    }

    public void Options()
    {
        Debug.Log("Loading options...");
    }

    public void Exit()
    {
        Application.Quit();

    }
}
