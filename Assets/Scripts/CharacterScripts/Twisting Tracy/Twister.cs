using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twister : Projectile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.Equals(owner) == false)
        {


            other.gameObject.GetComponent<CharacterAnimation>().Kill();
            if (gc) gc.UpdateScore(owner.GetComponent<Character>().playerID);
            owner.GetComponent<CharacterAnimation>().canAttack = false;
            owner.GetComponent<CharacterMovement>().canMove = false;
            other.gameObject.GetComponent<PhysicsPlugin>().UpdateVelocity(new Vector3(0, 0, 0));
            owner.GetComponent<PhysicsPlugin>().UpdateVelocity(new Vector3(0, 0, 0));

            Destroy(gameObject);

        }
    }

    
}
