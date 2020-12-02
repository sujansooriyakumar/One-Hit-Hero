using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceCondition : MonoBehaviour, ICondition
{
    StateMachine sm;
    public AdvanceCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
        bool result = false;


        if (sm.GetPlayerRef().GetComponent<Character>().GetCurrentState() == Character.PlayerState.RETREATING)
        {
            result = true;
        }

        return result;
    }
}
