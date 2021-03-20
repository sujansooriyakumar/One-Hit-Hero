using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialCondition : ICondition
{
    StateMachine sm;
    bool result = false;
    float totalTime = 0;
    public AerialCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {

        float test = Random.Range(0.0f, 1.0f);

        if (test < sm.aerialChance)
        {
            result = true;
        }

        else
        {
            result = false;
        }


        return result;
    }

    public void CheckResult()
    {

    }
}
