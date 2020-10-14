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

        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
            
        
    }

    [PunRPC]
    void CheckSpawned()
    {
        
    }
}
