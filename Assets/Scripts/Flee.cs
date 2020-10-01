using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{

    public GameObject target;
    float maxAccel;
    // Start is called before the first frame update
    void Start()
    {
        maxAccel = 7.0f;

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity += GetSteering() * Time.deltaTime;

    }

    Vector2 GetSteering()
    {
        Vector2 result;
        result = transform.position - target.transform.position;
        result.Normalize();
        result *= maxAccel;
        result.y = 0;
        return result;
    }
}
