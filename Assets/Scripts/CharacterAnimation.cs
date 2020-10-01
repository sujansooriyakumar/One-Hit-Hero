using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Rigidbody2D rb;
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
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
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
}
