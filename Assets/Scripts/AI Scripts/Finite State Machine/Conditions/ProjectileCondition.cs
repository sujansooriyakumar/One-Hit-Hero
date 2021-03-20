using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCondition : ICondition
{
    StateMachine sm;
    public ProjectileCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
        bool result = false;


        if(Random.Range(0.0f, 1.0f) < sm.projectileChance)
        {
            result = true;
        }
       

        return result;
    }
}

