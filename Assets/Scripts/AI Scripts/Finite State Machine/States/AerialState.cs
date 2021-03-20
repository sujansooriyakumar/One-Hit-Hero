using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialState : IState
{
    IAction[] exitActions;
    IAction[] entryActions;
    ITransition[] transitions;
    IAction[] actions;
    StateMachine sm;

    public AerialState(StateMachine sm_)
    {
        sm = sm_;
        exitActions = new IAction[1];
        exitActions[0] = new AerialAction(sm);
        entryActions = new IAction[1];
        entryActions[0] = new AerialAction(sm);
        actions = new IAction[1];
        actions[0] = new AerialAction(sm);
        transitions = new ITransition[3];
        transitions[0] = new ToInitial(sm);
        transitions[1] = new ToAdvance(sm);
        transitions[2] = new ToRetreat(sm);
     

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
