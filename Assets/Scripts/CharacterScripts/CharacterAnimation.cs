using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls the animation for the character
 * as well as attacks. 
 */
public class CharacterAnimation : MonoBehaviourPun
{
    public bool aerialHitbox;
    public bool antiAirHitbox;
    protected Animator anim;
    protected PhysicsPlugin rb;
    protected CharacterMovement moveController;
    protected GameObject projectileInst;
    public GameObject projectile, projectileSpawnLoc;
    public BoxCollider[] attackHitboxes;
    protected GameController gc;
    public bool canAttack;
    protected virtual void Awake()
    {
        canAttack = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<PhysicsPlugin>();
        moveController = GetComponent<CharacterMovement>();
        gc = GameObject.FindObjectOfType<GameController>();
    }
    protected virtual void Update()
    {
        if (aerialHitbox)
        {
            Aerial();
        }

        if (antiAirHitbox)
        {
            AntiAir();
        }

        // if player facing left
        if (transform.rotation.eulerAngles.y == 270)
        {
            if (rb.GetVelocity().x < 0)
            {
                anim.SetBool("Walk Forward", true);
                anim.SetBool("Walk Backward", false);
            }
            else if (rb.GetVelocity().x > 0)
            {
                anim.SetBool("Walk Forward", false);
                anim.SetBool("Walk Backward", true);
            }

            else
            {
                anim.SetBool("Walk Forward", false);
                anim.SetBool("Walk Backward", false);
            }
        }
        // if player facing right

        if (transform.rotation.eulerAngles.y == 90)
        {
            if(rb.GetVelocity().x > 0)
            {
                anim.SetBool("Walk Forward", true);
                anim.SetBool("Walk Backward", false);
            }
            else if (rb.GetVelocity().x < 0)
            {
                anim.SetBool("Walk Forward", false);
                anim.SetBool("Walk Backward", true);
            }

            else
            {
                anim.SetBool("Walk Forward", false);
                anim.SetBool("Walk Backward", false);
            }
        }
    }

    virtual public void AnimateSpecial(string specialName_)
    {
        switch (specialName_)
        {
            case "Projectile":
                if (projectileInst == null && canAttack)
                {
                    moveController.canMove = false;

                    anim.SetTrigger("RangeAttack1Trigger");
                    rb.UpdateVelocity(new Vector3(0, 0, 0));
                }
                break;
            case "AntiAir":
                if (canAttack)
                {
                    anim.SetTrigger("UppercutTrigger");
                    rb.UpdateVelocity(new Vector3(0, 0, 0));
                    moveController.canMove = false;
                    anim.SetBool("Grounded", false);
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

    virtual public void AnimateJump(float velocity_)
    {
        
        if (velocity_ == 0) anim.SetTrigger("JumpTrigger");
        if(transform.rotation.eulerAngles.y == 90)
        {
            // facing right
            if(rb.GetVelocity().x > 0)
            {
                anim.SetTrigger("JumpForwardTrigger");
            }

            if (rb.GetVelocity().x < 0)
            {
                anim.SetTrigger("JumpBackwardTrigger");
            }
        }

        if (transform.rotation.eulerAngles.y == 270)
        {
            // facing left
            if (rb.GetVelocity().x < 0)
            {
                anim.SetTrigger("JumpForwardTrigger");
            }

            if (rb.GetVelocity().x > 0)
            {
                anim.SetTrigger("JumpBackwardTrigger");
            }
        }
    }

    virtual public void SpawnProjectile()
    {
        if(projectileInst == null)
        {
            projectileInst = Instantiate(projectile, projectileSpawnLoc.transform.position, Quaternion.identity);
            projectileInst.GetComponent<Projectile>().owner = gameObject;
            Rigidbody projectileRB = projectileInst.GetComponent<Rigidbody>();
            if (transform.rotation.eulerAngles.y == 90) projectileRB.velocity = new Vector2(5.0f, 0.0f);
            else projectileRB.velocity = new Vector2(-5.0f, 0.0f);
        }
    }

    virtual protected void AntiAir()
    {
        anim.SetBool("Grounded", false);
        rb.UpdateVelocity(new Vector3(rb.GetVelocity().x, 5.0f, 0));
        moveController.SetIsGrounded(false);
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

    virtual protected void Aerial()
    {
        //Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[0].bounds.center, attackHitboxes[0].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        Collider[] cols = Physics.OverlapBox(attackHitboxes[1].bounds.center, attackHitboxes[1].bounds.extents, Quaternion.identity, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {

            if (c.gameObject != gameObject)
            {
               aerialHitbox = false;
               c.gameObject.GetComponent<CharacterAnimation>().Kill();
               gc.UpdateScore(GetComponent<Character>().playerID);
               moveController.canMove = false;
               canAttack = false;
            }
        }
    }

    virtual public void Kill()
    {
        anim.SetTrigger("DeathTrigger");

    }
  
    virtual public void SetAerialHitbox(int val_)
    {
        if(val_ == 1) aerialHitbox = true;
        if (val_ == 0) aerialHitbox = false;
    }

    virtual public void SetAntiAirHitbox(int val)
    {
        if (val == 1) antiAirHitbox = true;
        if (val == 0) antiAirHitbox = false;
    }

    public Animator GetAnimator()
    {
        return anim;
    }
    
  
}
