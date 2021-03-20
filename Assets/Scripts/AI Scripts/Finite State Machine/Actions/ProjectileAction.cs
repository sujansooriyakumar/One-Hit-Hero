using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAction : IAction
{
    bool active = false;
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
        //playerRef.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (playerRef.GetComponent<CharacterAnimation>().projectileInst == null)
        {
            playerRef.GetComponent<CharacterAnimation>().AnimateSpecial("Projectile");
            playerRef.GetComponent<CharacterAnimation>().canAttack = false;
            playerRef.GetComponent<CharacterMovement>().canMove = false;
        }
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
