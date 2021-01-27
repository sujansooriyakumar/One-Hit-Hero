using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistingTracyAnimation : CharacterAnimation
{
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

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();

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
        if (projectileInst == null)
        {
            projectileInst = Instantiate(projectile, projectileSpawnLoc.transform.position, Quaternion.identity);
            projectileInst.GetComponent<Projectile>().owner = gameObject;
            Rigidbody projectileRB = projectileInst.GetComponent<Rigidbody>();
            if (transform.rotation.eulerAngles.y == 90) projectileRB.velocity = new Vector2(2.0f, 0.0f);
            else projectileRB.velocity = new Vector2(-2.0f, 0.0f);
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
