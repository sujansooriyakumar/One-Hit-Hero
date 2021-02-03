using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviourPun
{
    public bool rematchReady;
    public enum PlayerState
    {
        ADVANCING,
        RETREATING,
        JUMPING,
        DEFAULT
    }
    PlayerState currentState;
    public bool isNetworked;
    private CharacterMovement moveController;
    private CharacterAnimation animationController;
    private PhysicsPlugin rb;
    private bool specialPressed;
    public int playerID;
    GameController gc;
    /*
     * This script reads all input, then delegates tasks to the required scripts
     */
    protected virtual void Awake()
    {
        rb = GetComponent<PhysicsPlugin>();
        moveController = GetComponent<CharacterMovement>();
        animationController = GetComponent<CharacterAnimation>();
        gc = FindObjectOfType<GameController>();
        rematchReady = false;
    }

    protected virtual void Start()
    {
        if (gc.isNetworked)
        {
            playerID = photonView.ViewID;
        }
        isNetworked = gc.isNetworked;
        currentState = PlayerState.DEFAULT;
    }

    protected virtual void Update()
    {
        UpdatePlayerState();
        if (specialPressed && moveController.GetIsGrounded() && moveController.GetVelocity().y == 0 && animationController.canAttack)
        {
            animationController.AnimateSpecial("Projectile");
            animationController.canAttack = false;
            specialPressed = false;
        }

        else if (specialPressed && moveController.GetIsGrounded() && animationController.canAttack)
        {
            animationController.AnimateSpecial("AntiAir");
            animationController.canAttack = false;

            specialPressed = false;
        }

        else if (specialPressed && GetComponent<CharacterMovement>().GetIsGrounded() == false && animationController.canAttack)
        {
            animationController.AnimateSpecial("Aerial");
            animationController.canAttack = false;

            specialPressed = false;
        }
        else
        {
            specialPressed = false;
        }

        if (PhotonNetwork.IsConnected) photonView.RPC("CheckDirection", RpcTarget.All);
        else
        {
            CheckDirection();
        }
    }
    public virtual void SpecialEvent(InputAction.CallbackContext context)
    {
        specialPressed = context.ReadValueAsButton();
    }

    public virtual void MoveEvent(InputAction.CallbackContext context)
    {
        moveController.SetVelocity(context.ReadValue<Vector2>());
      
    }

    [PunRPC]
    protected virtual void CheckDirection()
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
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (p2.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
        }
    }

    protected virtual void UpdatePlayerState()
    {
        PhysicsPlugin rb = GetComponent<PhysicsPlugin>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if((rb.GetVelocity().x > 0 && transform.localScale.x < 0) || (rb.GetVelocity().x < 0 && transform.localScale.x > 0))
        {
            currentState = PlayerState.ADVANCING;
        }

        else if ((rb.GetVelocity().x > 0 && transform.localScale.x > 0) || (rb.GetVelocity().x < 0 && transform.localScale.x < 0))
        {
            currentState = PlayerState.RETREATING;
        }
        

        else if(rb.GetVelocity().y > 0)
        {
            currentState = PlayerState.JUMPING;
        }
        else
        {
            currentState = PlayerState.DEFAULT;
        }
    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }

    [PunRPC]
    protected virtual void AwaitRematch()
    {
        rematchReady = true;
    }
}
