using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPun
{
    bool aSpawned, bSpawned = false;

    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private Transform SpawnLocA = null;
    [SerializeField] private Transform SpawnLocB = null;

    private void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, SpawnLocA.position, Quaternion.identity);
        }

        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name, SpawnLocB.position, Quaternion.identity);


        }





    }

    [PunRPC]
    void CheckSpawned()
    {
    }
}
