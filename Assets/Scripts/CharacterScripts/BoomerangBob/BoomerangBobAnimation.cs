using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBobAnimation : CharacterAnimation
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Aerial()
    {
        base.Aerial();
    }

    public override void AnimateJump(float velocity_)
    {
        base.AnimateJump(velocity_);
    }

    public override void AnimateSpecial(string specialName_)
    {
        base.AnimateSpecial(specialName_);
    }

    protected override void AntiAir()
    {
        base.AntiAir();
    }

    public override void Kill()
    {
        base.Kill();
    }

    public override void SetAerialHitbox(int val_)
    {
        base.SetAerialHitbox(val_);
    }

    public override void SetAntiAirHitbox(int val)
    {
        base.SetAntiAirHitbox(val);
    }

    public override void SpawnProjectile()
    {
        base.SpawnProjectile();
    }

     
} 
