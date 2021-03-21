using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviourPun
{
    // Start is called before the first frame update
    public bool isNetworked;
    public string characterSelection;
    public string characterSelectionp2;
    public bool paused;
    public int playerOneWins;
    public int playerTwoWins;
    public List<string> names;
    public string[] characterNames;
    public enum GameMode
    {
        ARCADE,
        OFFLINE,
        ONLINE,
        TRAINING
    }

    public GameMode currentMode;
    void Start()
    {
        DontDestroyOnLoad(this);
        
        playerOneWins = 0;
        paused = false;
        playerTwoWins = 0;
        characterNames = new string[4];
        names.Add("TwistingTracy");
        names.Add("BoomerangBob");
        names.Add("HeftyHarry");
        names.Add("Berserker");

    }
    private void Update()
    {
    }
    public void SetCharacter(string name_, int playerID)
    {
        if (playerID == 1) characterSelection = name_;
        if (currentMode == GameMode.ARCADE)
        {
            characterSelectionp2 = names.ToArray()[Random.Range(0, names.ToArray().Length)];
            names.Remove(characterSelectionp2);
            
        }
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

    public void UpdateScore(int playerID_)
    {
        if (playerID_ == 1001) playerOneWins++;
        else playerTwoWins++;
        if (playerOneWins >= 5 || playerTwoWins >= 5)
        {
            if (currentMode != GameMode.ARCADE)
            {
                playerOneWins = 0;
                playerTwoWins = 0;
                FindObjectOfType<PlayerSpawner>().gameOver.SetActive(true);
                Time.timeScale = 0.0f;
            }

            if(currentMode == GameMode.ARCADE)
            {
                if (names.Count > 0 && playerOneWins >= 5)
                {
                    characterSelectionp2 = names.ToArray()[Random.Range(0, names.Count)];
                    names.Remove(characterSelectionp2);
                    playerOneWins = 0;
                    playerTwoWins = 0;
                    Invoke("ResetPositions", 2.0f);
                }
                if(playerTwoWins >= 5)
                {
                    playerOneWins = 0;
                    playerTwoWins = 0;
                    FindObjectOfType<PlayerSpawner>().gameOver.SetActive(true);
                    Time.timeScale = 0.0f;
                }
               


            }
        }
        else
        {
            Invoke("ResetPositions", 2.0f);
        }
    }

    [PunRPC]
    public void ResetPositions()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Character[] players = GameObject.FindObjectsOfType<Character>();
        foreach(Character c in players)
        {
          
          

            if (playerOneWins < 5 && playerTwoWins < 5)
            {
                c.GetComponent<CharacterMovement>().canMove = true;
                c.GetComponent<CharacterAnimation>().canAttack = true;
            }
        }
        //Camera.main.transform.position = new Vector3(0, 0, -10);
    }


    public void MainMenu()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("NewMainMenu");
    }

    [PunRPC]
    public void Rematch()
    {
        ResetPositions();
        Time.timeScale = 1.0f;
        HideUI();
    }

    void HideUI()
    {
        FindObjectOfType<PlayerSpawner>().gameOver.SetActive(false);

    }


}
