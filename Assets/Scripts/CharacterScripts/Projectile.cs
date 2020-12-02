using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    public GameObject owner;
    GameController gc;

    private void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterAnimation>().Kill();
            if(gc)gc.UpdateScore(owner.GetComponent<Character>().playerID);
            //owner.GetComponent<CharacterAnimation>().canAttack = false;
            owner.GetComponent<CharacterMovement>().canMove = false;

        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

    }


    [PunRPC]

    void RemoveObject()
    {
    }
}
