using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviourPun
{
    public bool isNetworked;
    private CharacterMovement moveController;
    private CharacterAnimation animationController;
    private Rigidbody2D rb;
    private bool specialPressed;

    /*
     * This script reads all input, then delegates tasks to the required scripts
     */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveController = GetComponent<CharacterMovement>();
        animationController = GetComponent<CharacterAnimation>();
    }

    private void Update()
    {
        if (specialPressed && moveController.GetIsGrounded() && moveController.GetVelocity().y == 0)
        {
            animationController.AnimateSpecial("Projectile");
            specialPressed = false;
        }

        else if(specialPressed && moveController.GetIsGrounded() && moveController.GetVelocity().y < 0)
        {
            animationController.AnimateSpecial("AntiAir");
            specialPressed = false;
        }

        else if(specialPressed && moveController.GetIsGrounded() == false)
        {
            animationController.AnimateSpecial("Aerial");
            specialPressed = false;
        }

        if (PhotonNetwork.IsConnected) photonView.RPC("CheckDirection", RpcTarget.All);
    }
    virtual public void SpecialEvent(InputAction.CallbackContext context)
    {
        specialPressed = context.ReadValueAsButton();
    }

    public void MoveEvent(InputAction.CallbackContext context)
    {
        moveController.SetVelocity(context.ReadValue<Vector2>());
      
    }

    [PunRPC]
    private void CheckDirection()
    {
        Character p2 = null;
        Character[] players = GameObject.FindObjectsOfType<Character>();

        if (players.Length >= 2)
        {
            if (players[0] == this)
            {
                p2 = players[1];
            }

            else if (players[1] == this)
            {
                p2 = players[0];
            }

            if (p2.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-2, 2, 1);
            }
            else if (p2.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(2, 2, 1);
            }
        }
    }
}
