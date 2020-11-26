using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransition
{
    bool IsTriggered();
    IState GetTargetState();
    IAction[] GetActions();
   
}
