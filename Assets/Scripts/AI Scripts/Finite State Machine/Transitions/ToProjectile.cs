using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToProjectile : ITransition
{

    StateMachine sm;
    IState targetState;
    IAction[] actions;
    ICondition condition;

    public ToProjectile(StateMachine sm_)
    {
        sm = sm_;
        targetState = sm_.retreatState;
        condition = new ProjectileCondition(sm);
        actions = new IAction[1];
        actions[0] = new ProjectileAction(sm);
    }
    public IState GetTargetState()
    {
        return sm.projectileState;
    }

    public bool IsTriggered()
    {
        return condition.Test();
    }

    public IAction[] GetActions()
    {
        return actions;
    }


}
