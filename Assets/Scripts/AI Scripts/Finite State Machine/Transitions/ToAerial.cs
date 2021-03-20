using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToAerial : ITransition
{
    StateMachine sm;
    IState targetState;
    IAction[] actions;
    ICondition condition;
    public ToAerial(StateMachine sm_)
    {
        sm = sm_;
        targetState = sm.aerialState;
        condition = new AerialCondition(sm);
        actions = new IAction[1];
        actions[0] = new AerialAction(sm);
    }
    public IState GetTargetState()
    {
        return sm.aerialState;
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
