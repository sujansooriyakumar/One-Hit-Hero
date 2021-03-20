using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirCondition : ICondition
{
    StateMachine sm;
    bool result = false;
    float totalTime = 0;
    public AntiAirCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
        bool result = false;
        float test = Random.Range(0.0f, 1.0f);

        if (test < sm.dpChance)
        {
            result = true;
        }

        else
        {
            result = false;
        }
        return result;
    }
}
