using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatAction : IAction
{
    StateMachine sm;
    public RetreatAction(StateMachine sm_)
    {
        sm = sm_;
    }

    public void Update()
    {
    }

    public bool CanDoBoth(IAction otherAction)
    {
        return false;
    }

    public void Execute()
    {
        GameObject npcRef = sm.gameObject;
        if (sm.GetComponent<Flee>() == null)
        {
            npcRef.AddComponent<Flee>();
        }

        npcRef.GetComponent<Flee>().enabled = true;
        if(npcRef.GetComponent<Arrive>())npcRef.GetComponent<Arrive>().enabled = false;

    }

    public bool Interrupt()
    {
        return false;
    }

    public bool IsComplete()
    {
        return false;
    }


}
