using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCondition : MonoBehaviour, ICondition
{
    StateMachine sm;
    public ProjectileCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
        bool result = false;


        GameObject npcRef = sm.gameObject;
        float aggression = sm.aggression;
        if (aggression >= 15.0f && npcRef.GetComponent<CharacterMovement>().GetIsGrounded())
        {
            result = true;
            sm.aggression = 0;
        }

        return result;
    }
}

