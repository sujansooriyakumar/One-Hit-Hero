using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeftyHarryAnimation : CharacterAnimation
{
    public GameObject DPParticles;
    protected override void Awake()
    {
        base.Awake();
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
        switch (specialName_)
        {
            case "Projectile":
                if (projectileInst == null && canAttack)
                {
                    moveController.canMove = false;

                    anim.SetTrigger("RangeAttack1Trigger");
                    rb.velocity = Vector3.zero;
                }
                break;
            case "AntiAir":
                if (canAttack)
                {
                    anim.SetTrigger("UppercutTrigger");
                    rb.velocity = Vector3.zero;
                    moveController.canMove = false;
               
                    GetComponent<Character>().currentState = Character.PlayerState.AIRINVUL;
                }
                break;
            case "Aerial":
                if (canAttack)
                {
                    anim.SetTrigger("HighKickTrigger");
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
            if (c.gameObject != gameObject && c.GetComponent<Character>().currentState != Character.PlayerState.AIRINVUL)
            {

                c.gameObject.GetComponent<CharacterAnimation>().Kill();
                antiAirHitbox = false;
                gc.UpdateScore(GetComponent<Character>().playerID);
                canAttack = false;
            }
        }
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
            if (transform.rotation.eulerAngles.y == 90) projectileRB.velocity = new Vector2(10.0f, 0.0f);
            else projectileRB.velocity = new Vector2(-10.0f, 0.0f);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SpawnDP()
    {
        Instantiate(DPParticles, attackHitboxes[0].gameObject.transform.position, Quaternion.identity);
    }
}
