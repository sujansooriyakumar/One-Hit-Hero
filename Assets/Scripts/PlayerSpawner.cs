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
    GameController gc;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }
    private void Start()
    {

        if (gc.isNetworked)
        {

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(gc.GetCharacter(), SpawnLocA.position, Quaternion.identity);
            }

            else
            {
                PhotonNetwork.Instantiate(gc.GetCharacter(), SpawnLocB.position, Quaternion.identity);


            }
        }

        else
        {
            Instantiate(Resources.Load(gc.characterSelection), SpawnLocA.position, Quaternion.identity);
            Instantiate(Resources.Load(gc.characterSelectionp2), SpawnLocB.position, Quaternion.identity);
        }




    }

    [PunRPC]
    void CheckSpawned()
    {
    }
}
