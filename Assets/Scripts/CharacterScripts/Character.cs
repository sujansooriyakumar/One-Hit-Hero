using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public bool rematchReady;
    public enum PlayerState
    {
        ADVANCING,
        RETREATING,
        JUMPING,
        AIRINVUL,
        DEAD,
        DEFAULT
    }
    public PlayerState currentState;
    public bool isNetworked;
    private CharacterMovement moveController;
    private CharacterAnimation animationController;
    private Rigidbody rb;
    public bool specialPressed;
    public int playerID;
    public int jumpCount, projectileCount, aerialCount, dpCount;
    GameController gc;
    /*
     * This script reads all input, then delegates tasks to the required scripts
     */
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveController = GetComponent<CharacterMovement>();
        animationController = GetComponent<CharacterAnimation>();
        gc = FindObjectOfType<GameController>();
        rematchReady = false;
        jumpCount = 0;
        aerialCount = 0;
        projectileCount = 0;
        dpCount = 0;
    }

    protected virtual void Start()
    {
       
        isNetworked = gc.isNetworked;
        currentState = PlayerState.DEFAULT;
    }

    protected virtual void Update()
    {
        UpdatePlayerState();
        if (specialPressed && moveController.GetIsGrounded() && moveController.GetVelocity().y == 0 && animationController.canAttack && moveController.GetVelocity().x == 0)
        {
            animationController.AnimateSpecial("Projectile");
            animationController.canAttack = false;
            specialPressed = false;
            projectileCount++;
        }

        else if (specialPressed && moveController.GetIsGrounded() && animationController.canAttack && moveController.GetVelocity().x == 0)
        {
            animationController.AnimateSpecial("AntiAir");
            animationController.canAttack = false;

            specialPressed = false;
            dpCount++;
        }

        else if (specialPressed && GetComponent<CharacterMovement>().GetIsGrounded() == false && animationController.canAttack)
        {
            animationController.AnimateSpecial("Aerial");
            animationController.canAttack = false;

            specialPressed = false;
            aerialCount++;
        }
      
            specialPressed = false;
       

       
            CheckDirection();
        
    }
    public virtual void SpecialEvent(InputAction.CallbackContext context)
    {
        specialPressed = context.ReadValueAsButton();
    }

    public virtual void MoveEvent(InputAction.CallbackContext context)
    {
        if(moveController.canMove) moveController.SetVelocity(context.ReadValue<Vector2>());
        else
        {
            moveController.SetVelocity(Vector3.zero);
        }
      
    }

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
        if((rb.velocity.x > 0 && transform.localScale.x < 0) || (rb.velocity.x < 0 && transform.localScale.x > 0))
        {
            currentState = PlayerState.ADVANCING;
        }

        else if ((rb.velocity.x > 0 && transform.localScale.x > 0) || (rb.velocity.x < 0 && transform.localScale.x < 0))
        {
            currentState = PlayerState.RETREATING;
        }
        

        else if(rb.velocity.y > 0)
        {
            currentState = PlayerState.JUMPING;
        }
        else if(rb.velocity == Vector3.zero && currentState != PlayerState.AIRINVUL)
        {
           // currentState = PlayerState.DEFAULT;
        }
    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }

    protected virtual void AwaitRematch()
    {
        rematchReady = true;
    }
}
