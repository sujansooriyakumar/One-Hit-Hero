using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Projectile
{
    public int bounceCount;

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        bounceCount = 0;

    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 1, transform.rotation.eulerAngles.z);
    }


    protected override void OnCollisionEnter(Collision collision)
    {
        //base.OnCollisionEnter(collision);
        if(bounceCount >= 1)
        {
            
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Attack")
        {
            Destroy(gameObject);
        }
        GetComponent<Rigidbody>().velocity *= -2;
        bounceCount++;

    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
