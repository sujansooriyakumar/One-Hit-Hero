using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public string characterChoice;
    public Dropdown dropdown;
    
    private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int maxPlayersPerRoom = 2;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        PhotonNetwork.AutomaticallySyncScene = true;
        characterChoice = dropdown.options[dropdown.value].text;

    }


    public void SetCharacter()
    {
        characterChoice = dropdown.options[dropdown.value].text;
    }
    public void FindOpponent()
    {
        isConnecting = true;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;

        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected due to {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients are waitin for an opponent, creating a new room.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully joined a room");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount != maxPlayersPerRoom)
        {
            Debug.Log("Client is waiting for an opponent");
        }
        else
        {
            Debug.Log("Matching is ready to begin");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log("match is ready to begin");
            PhotonNetwork.LoadLevel("SampleScene");

        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.LeaveRoom();
        
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
