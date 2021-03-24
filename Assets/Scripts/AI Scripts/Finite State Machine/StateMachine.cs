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
    public IState currentState;
    public IState initialState, advanceState, retreatState, projectileState, jumpState, antiAirState, aerialState;
    public GameObject playerChar;
    public IAction[] actions;
    public float aggression;
    public float jumpChance;
    public float aerialChance;
    public float dpChance;
    public float projectileChance;
    public float idleChance, advanceChance, retreatChance;
    float totalTime;



    private void Start()
    {
        currentState = new InitialState(this);
        actions = new IAction[0];
       // playerChar = GameObject.FindGameObjectWithTag("Player");
        initialState = new InitialState(this);
        advanceState = new AdvanceState(this);
        retreatState = new RetreatState(this);
        projectileState = new ProjectileState(this);
        antiAirState = new AntiAirState(this);
        aerialState = new AerialState(this);
        jumpState = new JumpState(this);
        aggression = 0.0f;
        jumpChance = 0;
        aerialChance = 0;
        dpChance = 0;
        projectileChance = 0;
        idleChance = 0;
        retreatChance = 0;
        advanceChance = 0;
        totalTime = 0;

    }

    private void Update()
    {
        CheckPlayerState();
        
        stateName = currentState.ToString();
        totalTime += Time.deltaTime;
        if (totalTime >= 0.5f)
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
                    currentState.GetActions();


                }
                else
                {
                    currentState.GetActions();
                }
            }
            totalTime = 0.0f;
        }
        
       
    }

    public GameObject GetPlayerRef()
    {
        return playerChar;
    }

    public void CheckPlayerState()
    {
        if(playerChar.GetComponent<Character>().currentState == Character.PlayerState.ADVANCING)
        {
            advanceChance = Random.Range(0.4f, 1.0f);
            retreatChance = Random.Range(0.2f, 0.6f);
            idleChance = Random.Range(0.1f, 0.3f);
            projectileChance = 0.3f;
        }
        if (playerChar.GetComponent<Character>().currentState == Character.PlayerState.RETREATING)
        {
            retreatChance = Random.Range(0.4f, 1.0f);
            advanceChance = Random.Range(0.2f, 0.6f);
            idleChance = Random.Range(0.1f, 0.3f);
            projectileChance = 0.3f;
        }
        if (playerChar.GetComponent<Character>().currentState == Character.PlayerState.DEFAULT)
        {
            idleChance = Random.Range(0.4f, 1.0f);
            advanceChance = Random.Range(0.2f, 0.6f);
            retreatChance = Random.Range(0.1f, 0.3f);
            projectileChance = 0.3f;
        }
        jumpChance = (float)playerChar.GetComponent<Character>().projectileCount / Random.Range(7,15);
        dpChance = (float)playerChar.GetComponent<Character>().jumpCount / Random.Range(7,15);
        aerialChance = (float)playerChar.GetComponent<Character>().projectileCount / Random.Range(7, 15);
        
        

    }
}

