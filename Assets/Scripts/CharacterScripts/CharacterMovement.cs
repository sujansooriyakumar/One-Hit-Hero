using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*
 * Controls all character movement
 * including jumping and horizontal movement.
 */
public class CharacterMovement : MonoBehaviourPun
{
    PhysicsPlugin rb;
    Character character;
    CharacterAnimation animationController;
    private Vector3 velocity;
    public bool isGrounded;
    public bool canMove;
    public bool isPlayer;
    private float speed;

   

    private void Awake()
    {
        character = GetComponent<Character>();
        animationController = GetComponent<CharacterAnimation>();
        speed = 3.0f;
        canMove = true;
    }
    private void Start()
    {
        rb = GetComponent<PhysicsPlugin>();

    }
    private void FixedUpdate()
    {
        
        if (isPlayer)
        {
            if (FindObjectOfType<GameController>().isNetworked)
            {
                if (canMove && photonView.IsMine) rb.UpdateVelocity(new Vector3(velocity.x * speed, rb.GetVelocity().y, 0));
                if (!isGrounded && photonView.IsMine)
                {
                    rb.UpdateVelocity(new Vector3(velocity.x, rb.GetVelocity().y - 0.2f));
                }

            }

            else if (FindObjectOfType<GameController>().isNetworked == false)
            {
                if (canMove) rb.UpdateVelocity(new Vector3(velocity.x * speed, rb.GetVelocity().y, 0));
                if (!isGrounded)
                {
                    rb.UpdateVelocity(new Vector3(velocity.x, rb.GetVelocity().y - 0.2f));
                }

            }
        }

        else if (!isPlayer)
        {
            if (!isGrounded)
            {
                rb.UpdateVelocity(new Vector3(velocity.x, rb.GetVelocity().y - 0.2f));
            }
        }
        if (isGrounded && canMove && velocity.y > 0)
        {
            animationController.AnimateJump(rb.GetVelocity().x);
            GetComponent<Animator>().SetBool("Grounded", false);

            velocity.y = 0;
            canMove = false;

        }



    }

    private void LateUpdate()
    {
       
    }

    virtual public void Jump(float jumpForce_)
    {
       
            rb.UpdateVelocity(new Vector3(rb.GetVelocity().x, jumpForce_, 0));
            isGrounded = false;
            canMove = false;



        
    }

    virtual public Vector3 GetVelocity()
    {
        return velocity;
    }

    virtual public bool GetIsGrounded()
    {
        return isGrounded;
    }

    virtual public void SetVelocity(Vector3 velocity_)
    {
        velocity = velocity_;
    }

    virtual public void SetCanMove()
    {
        canMove = true;
    }



    virtual public void SetIsGrounded(bool isGrounded_)
    {
        isGrounded = isGrounded_;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            rb.UpdateVelocity(new Vector3(0, 0, 0));
            isGrounded = true;
            SetCanMove();


        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.UpdateVelocity(new Vector3(0, 0, 0));
            GetComponent<Animator>().SetBool("Grounded", true);

            isGrounded = true;
        }
    }

    public void Collision()
    {
    
    }

}
