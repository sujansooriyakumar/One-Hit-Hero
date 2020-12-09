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
    Animator anim;
    PhysicsPlugin rb;
    CharacterMovement moveController;
    private GameObject projectileInst;
    public GameObject projectile, projectileSpawnLoc;
    public BoxCollider2D[] attackHitboxes;
    GameController gc;
    public bool canAttack;
    private void Awake()
    {
        canAttack = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<PhysicsPlugin>();
        moveController = GetComponent<CharacterMovement>();
        gc = GameObject.FindObjectOfType<GameController>();
    }
    private void Update()
    {
        anim.SetBool("Grounded", moveController.GetIsGrounded());

        if(transform.localScale.x < 0)
        {
            anim.SetFloat("walkSpeed", GetComponent<PhysicsPlugin>().GetVelocity().x);

        }

        else if(transform.localScale.x > 0)
        {
            anim.SetFloat("walkSpeed", GetComponent<PhysicsPlugin>().GetVelocity().x * -1);

        }
    }

    virtual public void AnimateSpecial(string specialName_)
    {
        switch (specialName_)
        {
            case "Projectile":
                if(projectileInst == null && canAttack) anim.SetTrigger("Fireball");
                rb.UpdateVelocity(new Vector3(0, 0, 0));
                moveController.canMove = false;
                break;
            case "AntiAir":
                if(canAttack) anim.SetTrigger("AntiAir");
                rb.UpdateVelocity(new Vector3(0, 0, 0)); 
                moveController.canMove = false;
                break;
            case "Aerial":
                if(canAttack) anim.SetTrigger("Aerial");
                moveController.canMove = false;
                break;
            default:
                Debug.Log("default");
                break;
        }
    }

    virtual public void AnimateJump(float velocity_)
    {
        if (velocity_ == 0) anim.SetTrigger("Jump");
        else { anim.SetTrigger("JumpFwd"); }
    }

    virtual protected void SpawnProjectile()
    {
        if(projectileInst == null)
        {
            projectileInst = Instantiate(projectile, projectileSpawnLoc.transform.position, Quaternion.identity);
            projectileInst.GetComponent<Projectile>().owner = gameObject;
            Rigidbody2D projectileRB = projectileInst.GetComponent<Rigidbody2D>();
            if (transform.localScale.x < 0) projectileRB.velocity = new Vector2(5.0f, 0.0f);
            else projectileRB.velocity = new Vector2(-5.0f, 0.0f);
        }
    }

    virtual protected void AntiAir()
    {
        rb.UpdateVelocity(new Vector3(rb.GetVelocity().x, 10.0f, 0));
        moveController.SetIsGrounded(false);
        Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[1].bounds.center, attackHitboxes[1].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        foreach (Collider2D c in cols)
        {
            if (c.gameObject != gameObject)
            {
                c.gameObject.GetComponent<CharacterAnimation>().Kill();

                gc.UpdateScore(GetComponent<Character>().playerID);
                moveController.canMove = false;
                canAttack = false;
            }
        }
    }

    virtual protected void Aerial()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[0].bounds.center, attackHitboxes[0].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        foreach (Collider2D c in cols)
        {
            if (c.gameObject != gameObject)
            {
               c.gameObject.GetComponent<CharacterAnimation>().Kill();
               gc.UpdateScore(GetComponent<Character>().playerID);
                moveController.canMove = false;
                canAttack = false;
            }
        }
    }

    virtual public void Kill()
    {
        anim.SetTrigger("Hit");

    }
  
}
