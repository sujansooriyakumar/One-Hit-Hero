using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceCondition : ICondition
{
    StateMachine sm;
    bool result = false;
    float totalTime = 0;
    public AdvanceCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {

        float test = Random.Range(0.0f, 1.0f);
        totalTime += Time.deltaTime;
      
            if (test < sm.advanceChance)
            {
                result = true;
            }

            else
            {
                result = false;
            }
            totalTime = 0;
        

        return result;
    }

    public void CheckResult()
    {
        
    }
}
