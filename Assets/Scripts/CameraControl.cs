using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    // get reference to both players
    // get distance between both players
    // set a max distance between both players to enable panning
    // if the players are moving to the left, pan left
    // if the players are moving to the right, pan right
    // clamp edges of cam at -7 and 4
    GameObject leftPlayer, rightPlayer;
    float maxDist = 13.0f;
    float margin = 1.5f;
    public Transform _transform;
    Vector3 camPos;
    float xL, xR;
    float wScene;
    float z0 = 0;
    float zCam;

    private void Start()
    {
        camPos = _transform.position;
        FindPlayers();
        calcScreen();
        wScene = xR - xL;

        zCam = transform.position.z - z0;
    }

    private void FixedUpdate()
    {
        Vector3 newPos = transform.position;
        FindPlayers();
        calcScreen();
        float width = xR - xL;
        if (Vector3.Distance(leftPlayer.transform.position, rightPlayer.transform.position) < maxDist)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, (zCam * width) / (wScene + z0));
            if ((leftPlayer.GetComponent<Rigidbody>().velocity.x < -0.5f && rightPlayer.GetComponent<Rigidbody>().velocity.x < -0.5f))
            {
                newPos = new Vector3(Mathf.Clamp((xR + xL) / 2, -7.0f, 4.2f), transform.position.y, transform.position.z);

            }

            if ((leftPlayer.GetComponent<Rigidbody>().velocity.x > 0.5f && rightPlayer.GetComponent<Rigidbody>().velocity.x > 0.5f))
            {
                newPos = new Vector3(Mathf.Clamp((xR + xL) / 2, -7.0f, 4.2f), transform.position.y, transform.position.z);
            }
        }
        transform.position = Vector3.Lerp(transform.position, newPos, 1.0f * Time.deltaTime);
    }

    private void calcScreen()
    {
        xL = leftPlayer.transform.position.x - margin;
        xR = rightPlayer.transform.position.x + margin;
    }

    private void FindPlayers()
    {
        // function to determine which player is on right and which player is on left
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector3 p1Loc = players[0].transform.position;
        Vector3 p2Loc = players[1].transform.position;
        if (p1Loc.x > p2Loc.x)
        {
            leftPlayer = players[1];
            rightPlayer = players[0];
        }
        else if(p1Loc.x < p2Loc.x)
        {
            leftPlayer = players[0];
            rightPlayer = players[1];
        }
    }

}
