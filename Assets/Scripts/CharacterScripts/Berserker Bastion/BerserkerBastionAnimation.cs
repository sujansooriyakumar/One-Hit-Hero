﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerBastionAnimation : CharacterAnimation
{
    protected override void Aerial()
    {
        //Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[0].bounds.center, attackHitboxes[0].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        Collider[] cols = Physics.OverlapBox(attackHitboxes[1].bounds.center, attackHitboxes[1].bounds.extents, Quaternion.identity, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {

            if (c.gameObject != gameObject && c.GetComponent<Character>().currentState != Character.PlayerState.AIRINVUL)
            {
                aerialHitbox = false;
                c.gameObject.GetComponent<CharacterAnimation>().Kill();
                gc.UpdateScore(GetComponent<Character>().playerID);
                moveController.canMove = false;
                
                canAttack = false;
            }
        }
    }

    public override void AnimateJump(float velocity_)
    {
        base.AnimateJump(velocity_);
    }

    public override void AnimateSpecial(string specialName_)
    {
        switch (specialName_)
        {
            case "Projectile":
                if (projectileInst == null && canAttack)
                {
                    moveController.canMove = false;

                    anim.SetTrigger("RangeAttack1Trigger");
                    rb.velocity = (new Vector3(0, 0, 0));
                }
                break;
            case "AntiAir":
                if (canAttack)
                {
                    anim.SetTrigger("UppercutTrigger");
                    rb.velocity = (new Vector3(0, 0, 0));
                    moveController.canMove = false;
                    anim.SetBool("Grounded", false);
                }
                break;
            case "Aerial":
                if (canAttack)
                {
                    anim.SetTrigger("HighKickTrigger");
                    GetComponent<Rigidbody>().velocity = (new Vector3(0, -10, 0));
                    moveController.canMove = false;
                }
                break;
            default:
                Debug.Log("default");
                break;

        }
    }

    protected override void AntiAir()
    {
 
        moveController.canMove = false;
        // Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[1].bounds.center, attackHitboxes[1].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        Collider[] cols = Physics.OverlapBox(attackHitboxes[0].bounds.center, attackHitboxes[0].bounds.extents, Quaternion.identity, LayerMask.GetMask("Hitbox"));

        foreach (Collider c in cols)
        {
            if (c.gameObject != gameObject)
            {

                c.gameObject.GetComponent<CharacterAnimation>().Kill();
                antiAirHitbox = false;
                gc.UpdateScore(GetComponent<Character>().playerID);
                canAttack = false;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
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

    protected override void Update()
    {
        base.Update();
    }
}