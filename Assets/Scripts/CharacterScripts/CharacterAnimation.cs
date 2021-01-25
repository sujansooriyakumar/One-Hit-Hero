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
    bool aerialHitbox; 
    Animator anim;
    PhysicsPlugin rb;
    CharacterMovement moveController;
    private GameObject projectileInst;
    public GameObject projectile, projectileSpawnLoc;
    public BoxCollider[] attackHitboxes;
    PhysicsPlugin physics;
    GameController gc;
    public bool canAttack;
    private void Awake()
    {
        canAttack = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<PhysicsPlugin>();
        moveController = GetComponent<CharacterMovement>();
        gc = GameObject.FindObjectOfType<GameController>();
        physics = GetComponent<PhysicsPlugin>();
    }
    private void Update()
    {
        if (aerialHitbox)
        {
            Aerial();
        }

        // if player facing left
        if (transform.rotation.eulerAngles.y == 270)
        {
            if (physics.GetVelocity().x < 0)
            {
                anim.SetBool("Walk Forward", true);
                anim.SetBool("Walk Backward", false);
            }
            else if (physics.GetVelocity().x > 0)
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
            if(physics.GetVelocity().x > 0)
            {
                anim.SetBool("Walk Forward", true);
                anim.SetBool("Walk Backward", false);
            }
            else if (physics.GetVelocity().x < 0)
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
                if(projectileInst == null && canAttack) anim.SetTrigger("RangeAttack1Trigger");
                rb.UpdateVelocity(new Vector3(0, 0, 0));
                moveController.canMove = false;
                break;
            case "AntiAir":
                if(canAttack) anim.SetTrigger("UppercutTrigger");
                rb.UpdateVelocity(new Vector3(0, 0, 0)); 
                moveController.canMove = false;
                break;
            case "Aerial":
                if(canAttack) anim.SetTrigger("HighKickTrigger");
                moveController.canMove = false;
                break;
            default:
                Debug.Log("default");
                break;
        }
    }

    virtual public void AnimateJump(float velocity_)
    {
        
        if (velocity_ == 0) anim.SetTrigger("JumpTrigger");
        if((transform.localScale.x > 0 && physics.GetVelocity().x > 0) || (transform.localScale.x < 0 && physics.GetVelocity().x < 0)) 
        {
            anim.SetTrigger("JumpForwardTrigger");
        }
        if ((transform.localScale.x > 0 && physics.GetVelocity().x < 0) || (transform.localScale.x < 0 && physics.GetVelocity().x > 0))
        {
            anim.SetTrigger("JumpBackwardTrigger");
        }
    }

    virtual public void SpawnProjectile()
    {
        if(projectileInst == null)
        {
            projectileInst = Instantiate(projectile, projectileSpawnLoc.transform.position, Quaternion.identity);
            projectileInst.GetComponent<Projectile>().owner = gameObject;
            Rigidbody2D projectileRB = projectileInst.GetComponent<Rigidbody2D>();
            if (transform.rotation.eulerAngles.y == 90) projectileRB.velocity = new Vector2(5.0f, 0.0f);
            else projectileRB.velocity = new Vector2(-5.0f, 0.0f);
        }
    }

    virtual protected void AntiAir()
    {
        rb.UpdateVelocity(new Vector3(rb.GetVelocity().x, 10.0f, 0));
        moveController.SetIsGrounded(false);
        // Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[1].bounds.center, attackHitboxes[1].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        Collider[] cols = Physics.OverlapBox(attackHitboxes[0].bounds.center, attackHitboxes[0].bounds.extents, Quaternion.identity, LayerMask.GetMask("Hitbox"));

        foreach (Collider c in cols)
        {
            if (c.gameObject != gameObject)
            {
                Debug.Log(c.gameObject.name);

                c.gameObject.GetComponent<CharacterAnimation>().Kill();

                gc.UpdateScore(GetComponent<Character>().playerID);
                moveController.canMove = false;
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
                Debug.Log(c.gameObject.name);
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

    
  
}
