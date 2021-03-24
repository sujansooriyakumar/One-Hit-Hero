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
        if (playerRef.GetComponent<CharacterMovement>().GetIsGrounded() == false && playerRef.GetComponent<CharacterAnimation>().canAttack)
        {
            playerRef.GetComponent<CharacterAnimation>().AnimateSpecial("Aerial");
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
