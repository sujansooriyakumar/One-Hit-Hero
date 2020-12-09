using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    float maxAccel;
    float speed = 2.5f;
    float targetRadius = 5.0f;
    public Character target;
    // Start is called before the first frame update
    void Start()
    {
        maxAccel = 3.0f;
        Character[] players = FindObjectsOfType<Character>();
        if(players[0] == this)
        {
            target = players[1];
        }

        else
        {
            target = players[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CharacterMovement>().canMove) GetComponent<PhysicsPlugin>().UpdateVelocity(new Vector3(GetSteering().x, GetComponent<PhysicsPlugin>().GetVelocity().y, 0));

    }

    Vector2 GetSteering()
    {
        Vector2 result;
        result = target.transform.position - transform.position;

        if(result.magnitude < targetRadius)
        {

            return new Vector2(0, 0);
        }
        result.Normalize();
        result *= maxAccel;
        //result.y = 0;
        return result;
    }

}
