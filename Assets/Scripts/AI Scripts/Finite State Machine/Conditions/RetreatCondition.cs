using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatCondition : ICondition
{
    bool result = false;
    float totalTime = 0;
    StateMachine sm;
    public RetreatCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
       

        float test = Random.Range(0.0f, 1.0f);
        totalTime += Time.deltaTime;
        
            if (test < sm.retreatChance)
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
