using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialAction : MonoBehaviour, IAction
{
    StateMachine sm;
    public bool jumped;
    float t;
    public AerialAction(StateMachine sm_)
    {
        sm = sm_;
        t = 0.0f;
    }
    public bool CanDoBoth(IAction otherAction)
    {
        return false;
    }

    public void Execute()
    {

        GameObject playerRef = sm.gameObject;
        if (playerRef.GetComponent<CharacterMovement>().isGrounded)
        {
            playerRef.GetComponent<CharacterAnimation>().AnimateJump(playerRef.GetComponent<Rigidbody>().velocity.x);
            playerRef.GetComponent<CharacterMovement>().isGrounded = false;
            playerRef.GetComponent<Animator>().SetBool("Grounded", false);

        }
        t += Time.deltaTime;
       if(t >= 0.1f && playerRef.GetComponent<CharacterMovement>().isGrounded == false)
        {
            sm.gameObject.GetComponent<CharacterAnimation>().AnimateSpecial("Aerial");
            sm.gameObject.GetComponent<CharacterAnimation>().aerialHitbox = true;
            t = 0;
        }
    }

    public bool Interrupt()
    {
        return false;
    }

    public bool IsComplete()
    {
        return false;
    }

    public void Update()
    {
    }

    
}
