using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPParticles : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<ParticleSystem>().isStopped == true)
        {
            Destroy(gameObject);
        }
    }
}
