using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirAction : IAction
{

    StateMachine sm;
    bool executed = false;
    public AntiAirAction(StateMachine sm_)
    {
        sm = sm_;
        executed = false;
    }
    public bool CanDoBoth(IAction otherAction)
    {
        return false;
    }

    public void Execute()
    {
        GameObject playerRef = sm.gameObject;
        if (playerRef.GetComponent<CharacterMovement>().isGrounded && playerRef.GetComponent<CharacterAnimation>().canAttack)
        {
            playerRef.GetComponent<CharacterAnimation>().AnimateSpecial("AntiAir");
            playerRef.GetComponent<CharacterAnimation>().antiAirHitbox = true;
            
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
