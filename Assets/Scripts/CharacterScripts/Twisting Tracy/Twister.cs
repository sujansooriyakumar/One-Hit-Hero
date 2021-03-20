using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twister : Projectile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.Equals(owner) == false)
        {


            collision.gameObject.GetComponent<CharacterAnimation>().Kill();
            if (gc) gc.UpdateScore(owner.GetComponent<Character>().playerID);
            owner.GetComponent<CharacterAnimation>().canAttack = false;
            owner.GetComponent<CharacterMovement>().canMove = false;
            collision.gameObject.GetComponent<Rigidbody>().velocity = (new Vector3(0, 0, 0));
            owner.GetComponent<Rigidbody>().velocity = (new Vector3(0, 0, 0));

            Destroy(gameObject);

        }
        Destroy(gameObject);

    }

    protected override void OnTriggerEnter(Collider other)
    {
        
    }

    
}
