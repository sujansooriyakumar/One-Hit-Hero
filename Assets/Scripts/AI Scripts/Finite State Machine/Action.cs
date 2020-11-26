using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{

    void Update();

    bool Interrupt();
    bool CanDoBoth(IAction otherAction);
    bool IsComplete();

    void Execute();
}
