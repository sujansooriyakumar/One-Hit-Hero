using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : IState
{
    IAction[] exitActions;
    IAction[] entryActions;
    ITransition[] transitions;
    IAction[] actions;
    StateMachine sm;

    public RetreatState(StateMachine sm_)
    {
        sm = sm_;
        exitActions = new IAction[1];
        exitActions[0] = new RetreatAction(sm);
        entryActions = new IAction[1];
        entryActions[0] = new RetreatAction(sm);
        actions = new IAction[1];
        actions[0] = new RetreatAction(sm);
        transitions = new ITransition[4];
        transitions[1] = new ToInitial(sm);
        transitions[0] = new ToProjectile(sm_);
        transitions[2] = new ToJump(sm_);
        transitions[3] = new ToAntiAir(sm_);





    }
    public IAction[] GetExitActions()
    {
        return exitActions;
    }

    public IAction[] GetActions()
    {
        foreach (IAction a in actions)
        {
            a.Execute();
        }
        return actions;
    }

    public IAction[] GetEntryActions()
    {
        return entryActions;
    }

    public ITransition[] GetTransitions()
    {
        return transitions;
    }


}
