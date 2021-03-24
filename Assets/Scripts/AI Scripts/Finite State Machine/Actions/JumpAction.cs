using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : IAction
{
    StateMachine sm;
    bool result = false;
    float totalTime = 0;

    public JumpAction(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool CanDoBoth(IAction otherAction)
    {
        return false;
    }

    public void Execute()
    {
        GameObject npcRef = sm.gameObject;

        npcRef.GetComponent<CharacterAnimation>().AnimateJump(npcRef.GetComponent<Rigidbody>().velocity.x);
        npcRef.GetComponent<CharacterAnimation>().GetAnimator().SetBool("Grounded", false);
        npcRef.GetComponent<CharacterMovement>().canMove = false;


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
