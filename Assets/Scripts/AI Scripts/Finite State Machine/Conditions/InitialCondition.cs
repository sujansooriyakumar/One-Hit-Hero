using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCondition : ICondition
{
    StateMachine sm;
    public InitialCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
        bool result = false;

        if (sm.gameObject.GetComponent<CharacterMovement>().isGrounded)
        {
            result = true;
        }

        return result;
    }
}
