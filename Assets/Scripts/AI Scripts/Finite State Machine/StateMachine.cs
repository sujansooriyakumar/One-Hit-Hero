using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState targetState;
    IState currentState;
    public IAction[] actions;

    private void Start()
    {
        currentState = new InitialState(this);
        actions = new IAction[0];


    }

    private void Update()
    {
        // check and apply transitions, return a list of actions
        ITransition triggered = null;


        // store first transition that triggers
        if (currentState != null)
        {
            foreach (ITransition t in currentState.GetTransitions())
            {
                if (t.IsTriggered())
                {
                    triggered = t;
                    break;
                }
            }

            // check if we have a transition to fire
            if (triggered != null)
            {
                targetState = triggered.GetTargetState();
                
                actions = currentState.GetExitActions();

                // actions += triggered.getActions()
                IAction[] newActions = new IAction[actions.Length + triggered.GetActions().Length];
                actions.CopyTo(newActions, 0);
                triggered.GetActions().CopyTo(newActions, actions.Length);
                actions = newActions;
                newActions = null;

                // actions += targetState.getEntryActions()
                newActions = new IAction[actions.Length + targetState.GetEntryActions().Length];
                actions.CopyTo(newActions, 0);
                targetState.GetEntryActions().CopyTo(newActions, actions.Length);
                
                currentState = targetState;
            }
            else
            {
                currentState.GetActions();
            }
        }
    }
}
