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
        transitions = new ITransition[1];
        transitions[0] = new InitialToAdvance(sm_);
        exitActions = new IAction[1];
        exitActions[0] = new AdvanceAction(sm);
        entryActions = new IAction[1];
        entryActions[0] = new AdvanceAction(sm);
        actions = new IAction[1];
        actions[0] = new AdvanceAction(sm);


    }

    public IAction[] GetExitActions()
    {
        return exitActions;
    }

    public IAction[] GetActions()
    {
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