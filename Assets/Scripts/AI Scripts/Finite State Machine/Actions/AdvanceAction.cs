using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceAction : IAction
{
    StateMachine sm;
    public AdvanceAction(StateMachine sm_)
    {
        sm = sm_;
    }
   
    public  void Update()
    {
    }

    public  bool CanDoBoth(IAction otherAction)
    {
        return false;
    }

    public  void Execute()
    {
        GameObject npcRef = sm.gameObject;
        if(sm.GetComponent<Arrive>() == null)
        {
            npcRef.AddComponent<Arrive>();
        }

        npcRef.GetComponent<Arrive>().enabled = true;
    }

    public bool Interrupt()
    {
        return false;
    }

    public  bool IsComplete()
    {
        return false;
    }

    
}
