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
        Character[] players = FindObjectsOfType<Character>();


        if (Vector3.Distance(players[0].transform.position, players[1].transform.position) >= 5.0f)
        {
            result = true;
        }

        return result;
    }
}
