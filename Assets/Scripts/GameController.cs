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
    void Start()
    {
        DontDestroyOnLoad(this);
        playerOneWins = 0;
        paused = false;
        playerTwoWins = 0;

    }
    private void Update()
    {
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

    public void UpdateScore(int playerID_)
    {
        if (playerID_ == 1001) playerOneWins++;
        else playerTwoWins++;
        if (playerOneWins >= 5 || playerTwoWins >= 5)
        {
            playerOneWins = 0;
            playerTwoWins = 0;
            FindObjectOfType<PlayerSpawner>().gameOver.SetActive(true);
        }
        else
        {
            Invoke("ResetPositions", 2.0f);
        }
    }

    [PunRPC]
    public void ResetPositions()
    {
        Character[] players = GameObject.FindObjectsOfType<Character>();
        foreach(Character c in players)
        {
            if(c.playerID == 1001)
            {
                c.transform.position = FindObjectOfType<PlayerSpawner>().SpawnLocA.position;
            }

            else
            {
                c.transform.position = FindObjectOfType<PlayerSpawner>().SpawnLocB.position;
            }

            if (playerOneWins < 5 && playerTwoWins < 5)
            {
                c.GetComponent<CharacterMovement>().canMove = true;
                c.GetComponent<CharacterAnimation>().canAttack = true;
            }
        }
        Camera.main.transform.position = new Vector3(0, 0, -10);
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

        HideUI();
    }

    void HideUI()
    {
        FindObjectOfType<PlayerSpawner>().gameOver.SetActive(false);

    }


}
