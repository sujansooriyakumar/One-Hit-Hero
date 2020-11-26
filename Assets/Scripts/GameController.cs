using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isNetworked;
    public string characterSelection;
    public string characterSelectionp2;
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SetCharacter(string name_, int playerID)
    {
        if (playerID == 1) characterSelection = name_;
        else if (playerID == 2) characterSelectionp2 = name_;
        if(isNetworked) SceneManager.LoadScene(2);

        else if(characterSelection != "" && characterSelectionp2 != "")
        {
            SceneManager.LoadScene(3);
        }
        // if networked begin searching for opponent
    }

    public string GetCharacter()
    {
        return characterSelection;
    }
    
}
