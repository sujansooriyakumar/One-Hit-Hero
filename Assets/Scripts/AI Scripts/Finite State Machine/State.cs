using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{

    IAction[] GetActions();
    IAction[] GetEntryActions();
    IAction[] GetExitActions();
    ITransition[] GetTransitions();

}
