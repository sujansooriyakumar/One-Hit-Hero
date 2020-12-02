using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatCondition : MonoBehaviour, ICondition
{
    StateMachine sm;
    public RetreatCondition(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool Test()
    {
        bool result = false;


        if (sm.GetPlayerRef().GetComponent<Character>().GetCurrentState() == Character.PlayerState.ADVANCING)
        {
            result = true;
        }

        return result;
    }
}
