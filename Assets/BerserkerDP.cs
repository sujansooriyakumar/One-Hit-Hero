using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerDP : MonoBehaviour
{
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        GetComponent<VolumetricLines.VolumetricLineBehavior>().EndPos = new Vector3(0, GetComponent<VolumetricLines.VolumetricLineBehavior>().EndPos.y + 0.1f, 0);
        if(t >= 0.75f)
        {
            Destroy(gameObject);
        }
    }
}
