using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAction : IAction
{
    StateMachine sm;
    public ProjectileAction(StateMachine sm_)
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
        GameObject playerRef = sm.gameObject;
        playerRef.GetComponent<CharacterAnimation>().AnimateSpecial("Projectile");
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
