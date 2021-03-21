using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{

    IAction[] exitActions;
    IAction[] entryActions;
    ITransition[] transitions;
    IAction[] actions;
    StateMachine sm;

    public JumpState(StateMachine sm_)
    {
        sm = sm_;
        exitActions = new IAction[1];
        exitActions[0] = new JumpAction(sm);
        entryActions = new IAction[1];
        entryActions[0] = new JumpAction(sm);
        actions = new IAction[1];
        actions[0] = new JumpAction(sm);
        transitions = new ITransition[2];
        transitions[0] = new ToInitial(sm);
        transitions[1] = new ToAerial(sm);



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

    public IAction[] GetExitActions()
    {
        return exitActions;
    }

    public ITransition[] GetTransitions()
    {
        return transitions;
    }
}
