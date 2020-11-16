using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    CharacterMovement moveController;
    private GameObject projectileInst;
    public GameObject projectile, projectileSpawnLoc;
    public BoxCollider2D[] attackHitboxes;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        moveController = GetComponent<CharacterMovement>();
    }
    private void Update()
    {
        anim.SetBool("Grounded", moveController.GetIsGrounded());
        anim.SetFloat("walkSpeed", Mathf.Abs(rb.velocity.x));
    }

    virtual public void AnimateSpecial(string specialName_)
    {
        switch (specialName_)
        {
            case "Projectile":
                if(projectileInst == null) anim.SetTrigger("Fireball");
                rb.velocity = new Vector2(0, 0);
                moveController.canMove = false;
                break;
            case "AntiAir":
                anim.SetTrigger("AntiAir");
                rb.velocity = new Vector2(0, 0);
                moveController.canMove = false;
                break;
            case "Aerial":
                anim.SetTrigger("Aerial");
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
            Rigidbody2D projectileRB = projectileInst.GetComponent<Rigidbody2D>();
            if (transform.localScale.x < 0) projectileRB.velocity = new Vector2(5.0f, 0.0f);
            else projectileRB.velocity = new Vector2(-5.0f, 0.0f);
        }
    }

    virtual protected void AntiAir()
    {
        rb.velocity = new Vector2(rb.velocity.x, 10.0f);
        moveController.SetIsGrounded(false);
        Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[1].bounds.center, attackHitboxes[1].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        foreach (Collider2D c in cols)
        {
            if (c.gameObject != gameObject)
            {
                c.gameObject.GetComponent<CharacterAnimation>().Kill();
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
            }
        }
    }

    virtual public void Kill()
    {
        anim.SetTrigger("Hit");

    }
  
}
