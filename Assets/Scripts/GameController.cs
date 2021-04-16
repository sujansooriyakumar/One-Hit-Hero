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
    public bool paused;
    public int playerOneWins;
    public int playerTwoWins;
    public List<string> names;
    public string[] characterNames;
    public static GameController instance;
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
        names.Add("TwistingTracy");
        names.Add("BoomerangBob");
        names.Add("HeftyHarry");
        names.Add("Berserker");

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

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
                Projectile[] projectiles = FindObjectsOfType<Projectile>();
                foreach(Projectile p in projectiles)
                {
                    Destroy(p.gameObject);
                }
            }

            if(currentMode == GameMode.ARCADE)
            {
                if (names.Count > 0 && playerOneWins >= 5)
                {
                    characterSelectionp2 = names.ToArray()[Random.Range(0, names.Count)];
                    playerOneWins = 0;
                    playerTwoWins = 0;
                    Invoke("ResetPositions", 2.0f);
                }
                if(playerTwoWins >= 5)
                {
                    playerOneWins = 0;
                    playerTwoWins = 0;
                    FindObjectOfType<PlayerSpawner>().gameOver.SetActive(true);
                }
               
                if(names.Count == 0 && playerOneWins <= 5)
                {
                    playerOneWins = 0;
                    playerTwoWins = 0;
                    FindObjectOfType<PlayerSpawner>().gameOver.SetActive(true);
                }

            }
        }
        else
        {
            Invoke("ResetPositions", 2.0f);
        }
    }

    public void ResetPositions()
    {
        names.Remove(characterSelectionp2);

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
        // names = new List<string>(4);
        names.Clear();
        names.Add("TwistingTracy");
        names.Add("BoomerangBob");
        names.Add("HeftyHarry");
        names.Add("Berserker");
        SceneManager.LoadScene(0);
        
    }

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
