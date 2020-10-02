using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseCharacter : MonoBehaviour
{
    private enum GAMESTATES
    {
        IDLE,
        FIREBALL,
        DP,
        AERIAL,
        DEAD,
        WALK,
        JUMP,
        AIRINVUL
    }
    GAMESTATES currentState;
    bool specialPressed;
    Vector2 velocity;
    bool canWalk;
    public GameObject projectile, projectileSpawn;
    private GameObject projectileInst;
    private float walkSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isJumping;
    public BoxCollider2D[] attackHitboxes;
    // Start is called before the first frame update
    void Start()
    {
        currentState = GAMESTATES.IDLE;
        walkSpeed = 3.0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // walking animation

        if (rb.velocity.x != 0)
        {
            currentState = GAMESTATES.WALK;
            anim.SetBool("walkFwd", true);
        }

        if (rb.velocity.x == 0)
        {
            anim.SetBool("walkFwd", false);
            anim.SetBool("walkBack", false);
        }
        // attack animations

    }

    // fixed update for physics only
    private void FixedUpdate()
    {
        if (specialPressed && !isJumping && !projectileInst && velocity.y == 0 && currentState == GAMESTATES.IDLE)
        {
            currentState = GAMESTATES.FIREBALL;
            anim.SetTrigger("Fireball");
           rb.velocity = new Vector2(0, 0);
            canWalk = false;
            specialPressed = false;
        }

        if (specialPressed && velocity.y < 0 && !isJumping && currentState == GAMESTATES.IDLE)
        {
            currentState = GAMESTATES.DP;
            anim.SetTrigger("AntiAir");
            canWalk = false;
            specialPressed = false;

        }

        if (specialPressed && isJumping)
        {
            currentState = GAMESTATES.AERIAL;
            anim.SetTrigger("Aerial");
            specialPressed = false;

        }
        CheckDirection();

        // walk
        if (!isJumping && canWalk) rb.velocity = new Vector2(velocity.x * walkSpeed, rb.velocity.y);

        // jump
        if (velocity.y > 0 && !isJumping && canWalk)
        {
            if (rb.velocity.x == 0) anim.SetTrigger("Jump");
            else { anim.SetTrigger("JumpFwd"); }
            anim.SetBool("Grounded", false);
            rb.AddForce(new Vector2(0, 200), ForceMode2D.Force);
            isJumping = true;
        }

    }

    void AntiAir()
    {
        currentState = GAMESTATES.AIRINVUL;
        rb.AddForce(new Vector2(0, 200), ForceMode2D.Force);
        anim.SetBool("Grounded", false);
        Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[1].bounds.center, attackHitboxes[1].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        foreach (Collider2D c in cols)
        {
            if (c.gameObject != gameObject)
            {
                c.gameObject.GetComponent<BaseCharacter>().Kill();
            }
        }

    }

    void Fireball()
    {
        if (projectileInst == null)
        {
            projectileInst = Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
            Rigidbody2D projectileRB = projectileInst.GetComponent<Rigidbody2D>();
            if (transform.localScale.x == -1) projectileRB.velocity = new Vector2(5.0f, 0.0f);
            else projectileRB.velocity = new Vector2(-5.0f, 0.0f);
        }
    }

    void Aerial()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(attackHitboxes[0].bounds.center, attackHitboxes[0].bounds.extents, 1.0f, LayerMask.GetMask("Hitbox"));
        foreach (Collider2D c in cols)
        {
            if (c.gameObject != gameObject)
            {
                if (c.gameObject.GetComponent<BaseCharacter>().currentState != GAMESTATES.AIRINVUL) c.gameObject.GetComponent<BaseCharacter>().Kill();
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            anim.SetBool("Grounded", true);
        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            // anim.SetTrigger("Hit");
        }
    }

    void CanWalk()
    {
        canWalk = true;
        currentState = GAMESTATES.IDLE;
    }

    void CheckDirection()
    {
        BaseCharacter p2 = null;
        BaseCharacter[] players = GameObject.FindObjectsOfType<BaseCharacter>();
        if (players[0] != this) p2 = players[0];
        else if (players[1] != this) p2 = players[1];

        if (p2.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (p2.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


    }

    public void MoveEvent(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>();
        if (velocity.y != 0)
        {
            currentState = GAMESTATES.JUMP;
        }
    }

    public void SpecialEvent(InputAction.CallbackContext context)
    {

        specialPressed = context.ReadValueAsButton();
    }


    public void Kill()
    {
        anim.SetTrigger("Hit");
        canWalk = false;

    }

}


