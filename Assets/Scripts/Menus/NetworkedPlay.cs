using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkedPlay : MonoBehaviourPunCallbacks
{
    public Text textDisplay;

    private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int maxPlayersPerRoom = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        FindOpponent();

    }
    public void SetCharacter()
    {
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
        textDisplay.text = "Connected to Master";
        Debug.Log("Connected To Master");
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;

        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        textDisplay.text = $"Disconnected due to {cause}";
        Debug.Log($"Disconnected due to {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        textDisplay.text = "No clients are waiting for an opponent, creating a new room.";
        Debug.Log("No clients are waitin for an opponent, creating a new room.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });

    }

    public override void OnJoinedRoom()
    {
        textDisplay.text = "Client successfully joined a room";
        Debug.Log("Client successfully joined a room");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount != maxPlayersPerRoom)
        {
            textDisplay.text = "Client is waiting for an opponent.";
            Debug.Log("Client is waiting for an opponent");
        }
        else
        {
            textDisplay.text = "Matching is ready to begin.";
            Debug.Log("Matching is ready to begin");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            textDisplay.text = "Match is ready to begin.";
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

