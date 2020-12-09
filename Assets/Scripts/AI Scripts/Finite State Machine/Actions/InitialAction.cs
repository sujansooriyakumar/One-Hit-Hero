using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialAction : IAction
{
    StateMachine sm;
    public InitialAction(StateMachine sm_)
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
        if(npcRef.GetComponent<Arrive>()) npcRef.GetComponent<Arrive>().enabled = false;
        if(npcRef.GetComponent<Flee>()) npcRef.GetComponent<Flee>().enabled = false;
        npcRef.GetComponent<PhysicsPlugin>().UpdateVelocity(new Vector2(0, npcRef.GetComponent<PhysicsPlugin>().GetVelocity().y));
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
