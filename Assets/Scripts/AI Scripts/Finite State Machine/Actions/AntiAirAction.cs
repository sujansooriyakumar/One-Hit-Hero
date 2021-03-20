using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirAction : IAction
{

    StateMachine sm;
    public AntiAirAction(StateMachine sm_)
    {
        sm = sm_;
    }
    public bool CanDoBoth(IAction otherAction)
    {
        return false;
    }

    public void Execute()
    {
        GameObject playerRef = sm.gameObject;
        playerRef.GetComponent<CharacterAnimation>().AnimateSpecial("AntiAir");
        playerRef.GetComponent<CharacterAnimation>().antiAirHitbox = true;

    }

    public bool Interrupt()
    {
        return false;
    }

    public bool IsComplete()
    {
        return false;
    }

    public void Update()
    {
    }
}
