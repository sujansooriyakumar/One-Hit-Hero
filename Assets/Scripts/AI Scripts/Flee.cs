using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{

    public Character target;
    float maxAccel;
    // Start is called before the first frame update
    void Start()
    {
        maxAccel = 3.0f;
        Character[] players = FindObjectsOfType<Character>();
        if (players[0].GetComponent<Character>().playerID == GetComponent<Character>().playerID)
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
        if (GetComponent<CharacterMovement>().canMove) GetComponent<Rigidbody>().velocity = (new Vector3(GetSteering().x, GetComponent<Rigidbody>().velocity.y, 0));
    }

    Vector2 GetSteering()
    {
        Vector2 result;
        result = transform.position - target.transform.position;
        result.Normalize();
        result *= maxAccel;
       // result.y = 0;
        return result;
    }
}
