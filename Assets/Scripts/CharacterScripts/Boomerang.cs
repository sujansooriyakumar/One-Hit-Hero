using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Projectile
{
    public int bounceCount;
    bool flipped;
    Vector3 spawnPos;
    // Start is called before the first frame update


   
    protected override void Start()
    {
        base.Start();
        bounceCount = 0;
        spawnPos = transform.position;



    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 1, transform.rotation.eulerAngles.z);
        if(owner.transform.rotation.eulerAngles.y == 90 &&
            (transform.position.x - spawnPos.x >= 10.0f) && !flipped)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * -1;
            flipped = true;
        }

        if (owner.transform.rotation.eulerAngles.y == 270 &&
           (transform.position.x - spawnPos.x <= -10.0f) && !flipped)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * -1;
            flipped = true;
        }
    }


    protected override void OnCollisionEnter(Collision collision)
    {
         base.OnCollisionEnter(collision);
        
       
     

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.Equals(owner))
            {
                Destroy(gameObject);
            }
            else
            {
                other.gameObject.GetComponent<CharacterAnimation>().Kill();
                if (gc) gc.UpdateScore(owner.GetComponent<Character>().playerID);
                owner.GetComponent<CharacterAnimation>().canAttack = false;
                owner.GetComponent<CharacterMovement>().canMove = false;
                owner.GetComponent<PhysicsPlugin>().UpdateVelocity(new Vector3(0, 0, 0));
                other.gameObject.GetComponent<PhysicsPlugin>().UpdateVelocity(new Vector3(0, 0, 0));
                Destroy(gameObject);
            }

        }
    }

}
