using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<BaseCharacter>().Kill();
        }
    }
    
}
