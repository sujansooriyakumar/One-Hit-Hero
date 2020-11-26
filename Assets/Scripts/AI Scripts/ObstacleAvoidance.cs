using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{

    float lookAhead;
    public GameObject target;
    Rigidbody2D rb;
    Animator anim;
    public Transform rayOrigin;
    public bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        lookAhead = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.position, target.transform.position - transform.position, lookAhead);
        // fire ray from player towards facing direction
        if (hit.collider!= null)
        {
            if (hit.collider.gameObject.tag == "Attack" && !isJumping)
            {

                if (rb.velocity.x == 0) anim.SetTrigger("Jump");
                else { anim.SetTrigger("JumpFwd"); }
                rb.AddForce(new Vector2(0, 200), ForceMode2D.Force);
                isJumping = true;
                
            }
        }
        // if hits object of type projectile
        // jump

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
