/*
  *  Sources
  *  GAME 307 Module 10, Prof. Gail Harris
  *  AI For Games, Third Edition, Ian Millington
  * 
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public string stateName;
    IState targetState;
    IState currentState;
    public IState initialState, advanceState, retreatState, projectileState;
    GameObject playerChar;
    public IAction[] actions;
    public float aggression;



    private void Start()
    {
        currentState = new InitialState(this);
        actions = new IAction[0];
        playerChar = GameObject.FindGameObjectWithTag("Player");
        initialState = new InitialState(this);
        advanceState = new AdvanceState(this);
        retreatState = new RetreatState(this);
        projectileState = new ProjectileState(this);
        aggression = 0.0f;

    }

    private void Update()
    {
        stateName = currentState.ToString();
        aggression += Time.deltaTime;
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
                currentState.GetActions();


            }
            else
            {
                currentState.GetActions();
            }
        }
    }

    public GameObject GetPlayerRef()
    {
        return playerChar;
    }
}
