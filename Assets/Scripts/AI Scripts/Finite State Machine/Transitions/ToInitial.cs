using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInitial : ITransition
{

    StateMachine sm;
    IState targetState;
    IAction[] actions;
    ICondition condition;
    public ToInitial(StateMachine sm_)
    {
        sm = sm_;
        targetState = sm.initialState;
        condition = new InitialCondition(sm);
        actions = new IAction[1];
        actions[0] = new AdvanceAction(sm);
    }
    public IState GetTargetState()
    {
        return sm.initialState;
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
