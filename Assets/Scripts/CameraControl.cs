using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class CameraControl : MonoBehaviourPun
{
    // get reference to both players
    // get distance between both players
    // the lower the distance, the more zoomed in the camera is
    // set a max distance between both players to enable panning
    // if the players are moving to the left, pan left
    // if the players are moving to the right, pan right
    // clamp edges of cam
    public Character[] playersRef;
    Character leftPlayer, rightPlayer;
    float distance;
    float maxDistance = 6.0f;
    float minX;
    float maxX;
    private void Awake()
    {
        distance = 0;
        minX = -1.5f;
        maxX = 3.85f;
    }

    private void Update()
    {
        if(FindObjectOfType<GameController>().isNetworked) photonView.RPC("GetPlayerRefs", RpcTarget.All);
        if (playersRef.Length >= 2)
        {
            float distance = Vector3.Distance(playersRef[0].transform.position, playersRef[1].transform.position);
            if(playersRef[0].transform.position.x < playersRef[1].transform.position.x)
            {
                leftPlayer = playersRef[0];
                rightPlayer = playersRef[1];
            }

            else
            {
                leftPlayer = playersRef[1];
                rightPlayer = playersRef[0];
            }

            if (distance < maxDistance)
            {
                if (leftPlayer.GetComponent<PhysicsPlugin>().GetVelocity().x > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(maxX, 0, transform.position.z), Time.deltaTime);
                }
                if (leftPlayer.GetComponent<PhysicsPlugin>().GetVelocity().x < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(minX, 0, transform.position.z), Time.deltaTime);
                }
                if (rightPlayer.GetComponent<PhysicsPlugin>().GetVelocity().x < 0)

                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(minX, 0, transform.position.z), Time.deltaTime);
                }

                if (rightPlayer.GetComponent<PhysicsPlugin>().GetVelocity().x > 0)

                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(maxX, 0, transform.position.z), Time.deltaTime);
                }
            }
        }

        // check if distance is less than maxdistance
        // if the player is moving to the left, pan left
        // if the player is moving to the right, pan right

       



    }

    [PunRPC]
    void GetPlayerRefs()
    {
        playersRef = FindObjectsOfType<Character>();
    }

}
