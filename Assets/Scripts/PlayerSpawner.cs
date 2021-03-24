using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject gameOver;
    bool aSpawned, bSpawned = false;
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] public Transform SpawnLocA = null;
    [SerializeField] public Transform SpawnLocB = null;
    [SerializeField] GameObject p1, p2;
    GameController gc;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
   

        
            p1 = Instantiate<GameObject>(Resources.Load<GameObject>( gc.characterSelection), SpawnLocA.position, Quaternion.identity);
            p1.GetComponent<Character>().playerID = 1001;
            p2 = Instantiate<GameObject>(Resources.Load<GameObject>(gc.characterSelectionp2), SpawnLocB.position, Quaternion.identity);
            p2.GetComponent<Character>().playerID = 2001;
        

        if (gc.currentMode == GameController.GameMode.ARCADE)
        {
            p2.AddComponent<StateMachine>();
            p2.GetComponent<StateMachine>().playerChar = p1;
            p2.GetComponent<CharacterMovement>().isPlayer = false;
        }


    }


    void CheckSpawned()
    {
    }
}
