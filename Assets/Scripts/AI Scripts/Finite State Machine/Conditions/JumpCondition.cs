using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCondition : ICondition
{


    StateMachine sm;
    bool result = false;
    float totalTime = 0;
    public JumpCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
        bool result = false;

        if(Random.Range(0.0f, 1.0f) < sm.jumpChance)
        {
            result = true;
        }

        return result;
    }
}
