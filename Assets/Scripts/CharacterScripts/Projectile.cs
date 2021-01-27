using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    public GameObject owner;
    protected GameController gc;

    protected virtual void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterAnimation>().Kill();
            if(gc)gc.UpdateScore(owner.GetComponent<Character>().playerID);
            //owner.GetComponent<CharacterAnimation>().canAttack = false;
            owner.GetComponent<CharacterMovement>().canMove= false;

        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterAnimation>().Kill();
            if (gc) gc.UpdateScore(owner.GetComponent<Character>().playerID);
            //owner.GetComponent<CharacterAnimation>().canAttack = false;
            owner.GetComponent<CharacterMovement>().canMove = false;
            other.gameObject.GetComponent<PhysicsPlugin>().UpdateVelocity(new Vector3(0, 0, 0));
            Destroy(gameObject);

        }



    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


    [PunRPC]

    void RemoveObject()
    {
    }
}
