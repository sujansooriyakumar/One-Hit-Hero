using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Projectile
{
    float t;
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject != owner)
        {
            collision.gameObject.GetComponent<CharacterAnimation>().Kill();
            if (gc) gc.UpdateScore(owner.GetComponent<Character>().playerID);
            //owner.GetComponent<CharacterAnimation>().canAttack = false;
            owner.GetComponent<CharacterMovement>().canMove = false;
            collision.gameObject.GetComponent<Rigidbody>().velocity = (new Vector3(0, 0, 0));
            owner.GetComponent<CharacterAnimation>().canAttack = false;
            owner.GetComponent<CharacterMovement>().canMove = false;
            owner.GetComponent<Rigidbody>().velocity = (new Vector3(0, 0, 0));


        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void Start()
    {
        base.Start();
        t = 0;
    }

    private void Update()
    {
        t += Time.deltaTime;
        if(t >= 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
