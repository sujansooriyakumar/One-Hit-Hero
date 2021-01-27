using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoomerangBob : Character
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void UpdatePlayerState()
    {
        base.UpdatePlayerState();
    }

    protected override void AwaitRematch()
    {
        base.AwaitRematch();
    }

    protected override void CheckDirection()
    {
        base.CheckDirection();
    }

    public override void MoveEvent(InputAction.CallbackContext context)
    {
        base.MoveEvent(context);
    }

    public override void SpecialEvent(InputAction.CallbackContext context)
    {
        base.SpecialEvent(context);
    }

    
}
