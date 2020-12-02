using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToRetreat : ITransition
{

    StateMachine sm;
    IState targetState;
    IAction[] actions;
    ICondition condition;

    public ToRetreat(StateMachine sm_)
    {
        sm = sm_;
        targetState = sm_.retreatState;
        condition = new RetreatCondition(sm);
        actions = new IAction[1];
        actions[0] = new RetreatAction(sm);
    }
    public IState GetTargetState()
    {
        return sm.retreatState;
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
