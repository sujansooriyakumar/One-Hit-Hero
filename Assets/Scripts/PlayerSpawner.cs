using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPun
{
    bool aSpawned, bSpawned = false;
    MainMenu controller;
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private Transform SpawnLocA = null;
    [SerializeField] private Transform SpawnLocB = null;

    private void Start()
    {
        controller = FindObjectOfType<MainMenu>();


        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(controller.characterChoice, SpawnLocA.position, Quaternion.identity);
        }

        else
        {
            PhotonNetwork.Instantiate(controller.characterChoice, SpawnLocB.position, Quaternion.identity);


        }





    }

    [PunRPC]
    void CheckSpawned()
    {
    }
}
