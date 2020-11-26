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
    Rigidbody2D rb;
    Character character;
    CharacterAnimation animationController;
    private Vector3 velocity;
    private bool isGrounded;
    public bool canMove;
    private float speed;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        animationController = GetComponent<CharacterAnimation>();
        speed = 2.0f;
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (character.isNetworked)
        {
            if (canMove && photonView.IsMine) rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y);
        }

        else
        {
            if(canMove) rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y);
        }
        if(isGrounded && canMove && velocity.y > 0)
        {
            Jump(10.0f);
        }
    }

    virtual public void Jump(float jumpForce_)
    {
        if (velocity.y > 0 && isGrounded && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce_);
            
            isGrounded = false;
            canMove = false;
            animationController.AnimateJump(rb.velocity.x);
            
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    virtual public void SetIsGrounded(bool isGrounded_)
    {
        isGrounded = isGrounded_;
    }
    
}
