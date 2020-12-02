using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCondition : MonoBehaviour, ICondition
{
    StateMachine sm;
    public InitialCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
        bool result = false;


        if (sm.GetPlayerRef().GetComponent<Character>().GetCurrentState() == Character.PlayerState.DEFAULT)
        {
            result = true;
        }

        return result;
    }
}
