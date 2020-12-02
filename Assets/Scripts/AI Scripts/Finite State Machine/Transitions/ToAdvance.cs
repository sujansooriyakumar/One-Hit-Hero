using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToAdvance : ITransition
{

    StateMachine sm;
    IState targetState;
    IAction[] actions;
    ICondition condition;
    public ToAdvance(StateMachine sm_)
    {
        sm = sm_;
        targetState = sm.advanceState;
        condition = new AdvanceCondition(sm);
        actions = new IAction[1];
        actions[0] = new AdvanceAction(sm);
    }
    public  IState GetTargetState()
    {
        return sm.advanceState;
    }

    public  bool IsTriggered()
    {
        return condition.Test();
    }

    public  IAction[] GetActions()
    {
        return actions;
    }
}
