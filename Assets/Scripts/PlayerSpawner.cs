﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPun
{
    bool aSpawned, bSpawned = false;
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] public Transform SpawnLocA = null;
    [SerializeField] public Transform SpawnLocB = null;
    GameObject p1, p2;
    GameController gc;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        if (gc.isNetworked)
        {

            if (PhotonNetwork.IsMasterClient)
            {
                p1 = PhotonNetwork.Instantiate(gc.GetCharacter(), SpawnLocA.position, Quaternion.identity);
                p1.GetComponent<Character>().playerID = 1;
            }

            else
            {
                p2 = PhotonNetwork.Instantiate(gc.GetCharacter(), SpawnLocB.position, Quaternion.identity);
                p2.GetComponent<Character>().playerID = 2;


            }
        }

        else
        {
            p1 = Instantiate<GameObject>(Resources.Load<GameObject>(gc.characterSelection), SpawnLocA.position, Quaternion.identity);
            p1.GetComponent<Character>().playerID = 1001;
            p2 = Instantiate<GameObject>(Resources.Load<GameObject>(gc.characterSelectionp2), SpawnLocB.position, Quaternion.identity);
            p2.GetComponent<Character>().playerID = 2001;
        }
    }
    private void Start()
    {
        
    }

    [PunRPC]
    void CheckSpawned()
    {
    }
}
