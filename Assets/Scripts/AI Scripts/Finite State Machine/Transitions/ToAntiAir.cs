using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToAntiAir : ITransition
{

    StateMachine sm;
    IState targetState;
    IAction[] actions;
    ICondition condition;
    public ToAntiAir(StateMachine sm_)
    {
        sm = sm_;
        targetState = sm.advanceState;
        condition = new AntiAirCondition(sm);
        actions = new IAction[1];
        actions[0] = new AntiAirAction(sm);
    }
    public IAction[] GetActions()
    {
        return actions;
    }

    public IState GetTargetState()
    {
        return sm.antiAirState;
    }

    public bool IsTriggered()
    {
        return condition.Test();
    }
}
