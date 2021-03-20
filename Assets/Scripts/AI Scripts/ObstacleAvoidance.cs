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
        lookAhead = 5.0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Projectile[] projectiles = FindObjectsOfType<Projectile>();
        foreach(Projectile p in projectiles)
        {
            if(p.owner != this.gameObject)
            {
                target = p.gameObject;
            }
        }
        if (target != null)
        {
            CheckCollision();
        }
    }

    void CheckCollision()
    {
        RaycastHit hit;
        // fire ray from player towards facing direction
        int layerMask = 1 <<10;
        if (Physics.Raycast(GetComponent<CharacterAnimation>().projectileSpawnLoc.transform.position, transform.TransformDirection(Vector3.forward), out hit, lookAhead, layerMask))
        {
            if (GetComponent<CharacterMovement>().isGrounded)
            {
                GetComponent<CharacterAnimation>().AnimateJump(GetComponent<Rigidbody>().velocity.x);
                GetComponent<CharacterMovement>().isGrounded = false;
                GetComponent<Animator>().SetBool("Grounded", false);
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
