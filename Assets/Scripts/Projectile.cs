using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterAnimation>().Kill();

        }
        Destroy(gameObject);
    }
    

    [PunRPC]

    void RemoveObject()
    {
    }
}
