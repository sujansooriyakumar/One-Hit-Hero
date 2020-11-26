using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialToAdvance : ITransition
{

    StateMachine sm;
    IState targetState;
    IAction[] actions;
    ICondition condition;

    public InitialToAdvance(StateMachine sm_)
    {
        sm = sm_;
        targetState = new AdvanceState(sm);
        condition = new AdvanceCondition(sm);
        actions = new IAction[1];
        actions[0] = new AdvanceAction(sm);
    }
    public  IState GetTargetState()
    {
        return targetState;
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
