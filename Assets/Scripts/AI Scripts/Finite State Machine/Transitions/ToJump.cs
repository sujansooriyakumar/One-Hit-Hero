using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJump : ITransition
{
    StateMachine sm;
    IState targetState;
    IAction[] actions;
    ICondition condition;
    public ToJump(StateMachine sm_)
    {
        sm = sm_;
        targetState = sm.jumpState;
        condition = new JumpCondition(sm);
        actions = new IAction[1];
        actions[0] = new JumpAction(sm);
    }
    public IAction[] GetActions()
    {
        return actions;
    }

    public IState GetTargetState()
    {
        return sm.jumpState;
    }

    public bool IsTriggered()
    {
        return condition.Test();
    }
}
