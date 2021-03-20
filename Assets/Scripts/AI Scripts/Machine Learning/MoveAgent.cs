using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveAgent : Agent
{

    [SerializeField] private GameObject target;
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(target.transform.position);
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation);
        sensor.AddObservation(GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(target.transform.rotation);
        
        
    }



    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];

        GetComponent<Rigidbody>().velocity = new Vector3(moveX * 5, GetComponent<Rigidbody>().velocity.y, 0);
    }


   
    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }

    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SetReward(1.0f);
            EndEpisode();
        }

        if (collision.gameObject.tag == "Wall")
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }


}
