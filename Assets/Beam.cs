using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Projectile
{
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), out hit, Mathf.Infinity)) { 
            if(hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<CharacterAnimation>().Kill();
            }
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
