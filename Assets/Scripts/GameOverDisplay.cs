using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{
    public string winner;

    private void Update()
    {
        GetComponent<Text>().text = winner + " Wins";
    }
}
