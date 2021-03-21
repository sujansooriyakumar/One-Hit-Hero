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
	Rigidbody rb;
	Character character;
	CharacterAnimation animationController;
	public Vector3 velocity;
	public bool isGrounded;
	public bool canMove;
	public bool isPlayer;
	protected float speed;



	protected virtual void Awake()
	{
		character = GetComponent<Character>();
		animationController = GetComponent<CharacterAnimation>();
		speed = 3.0f;
		canMove = true;
	}
	protected virtual void Start()
	{
		rb = GetComponent<Rigidbody>();

	}
	protected virtual void FixedUpdate()
	{
		if (isPlayer)
		{

			if (canMove) rb.velocity = (new Vector3(velocity.x * speed, rb.velocity.y, 0));





		}


		if (isGrounded && canMove && velocity.y > 0)
		{
			animationController.AnimateJump(rb.velocity.x);
			GetComponent<CharacterAnimation>().GetAnimator().SetBool("Grounded", false);

			velocity.y = 0;
			canMove = false;

		}



	}



	virtual public void Jump(float jumpForce_)
	{

		rb.velocity += (new Vector3(rb.velocity.x, jumpForce_, 0));
		isGrounded = false;
        GetComponent<Animator>().SetBool("Grounded", false);
		canMove = false;
		GetComponent<Character>().jumpCount += 1;



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
		animationController.canAttack = true;
        GetComponent<Character>().currentState = Character.PlayerState.DEFAULT;
	}



	virtual public void SetIsGrounded(bool isGrounded_)
	{
		isGrounded = isGrounded_;
	}




	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			GetComponent<CharacterAnimation>().GetAnimator().SetBool("Grounded", true);

			isGrounded = true;
            if (GetComponent<StateMachine>())
            {
                if(GetComponent<StateMachine>().currentState == GetComponent<StateMachine>().jumpState){
                    GetComponent<StateMachine>().currentState = GetComponent<StateMachine>().initialState;
                }
            }
		}
	}




}
