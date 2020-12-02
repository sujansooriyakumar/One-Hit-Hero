using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : IState
{
    IAction[] exitActions;
    IAction[] entryActions;
    ITransition[] transitions;
    IAction[] actions;
    StateMachine sm;

    public InitialState(StateMachine sm_)
    {
        sm = sm_;
        transitions = new ITransition[3];
        transitions[0] = new ToAdvance(sm_);
        transitions[1] = new ToRetreat(sm_);
        transitions[2] = new ToProjectile(sm_);
        exitActions = new IAction[1];
        exitActions[0] = new InitialAction(sm);
        entryActions = new IAction[1];
        entryActions[0] = new InitialAction(sm);
        actions = new IAction[1];
        actions[0] = new InitialAction(sm);


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