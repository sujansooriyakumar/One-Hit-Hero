using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2ScoreDisplay : MonoBehaviour
{
    GameController gc;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Wins: " + gc.playerTwoWins;
    }
}
