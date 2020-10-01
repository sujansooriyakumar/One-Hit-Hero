using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    float maxAccel;
    float speed = 2.5f;
    float targetRadius = 2.0f;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        maxAccel = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetSteering().x, GetComponent<Rigidbody2D>().velocity.y);

    }

    Vector2 GetSteering()
    {
        Vector2 result;
        result = target.transform.position - transform.position;
        if(result.magnitude < targetRadius)
        {
            Debug.Log(result);

            return new Vector2(0, 0);
        }
        result.Normalize();
        result *= maxAccel;
        //result.y = 0;
        return result;
    }

}
