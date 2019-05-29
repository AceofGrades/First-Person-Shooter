using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance;

    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;

        // Step 1: Check if we have valid target
        // If target is null
        if(target)
        {
            // Step 2: Get direction we want to go
            // SET desiredForce to target - current
            Vector3 desiredForce = target.position - transform.position;

            // Step 3: Apply weighting to desiredForce
            // IF desiredForce distance is greater than stoppingDistance
            if(desiredForce.magnitude > stoppingDistance)
            {
                // SET desiredForce to restricted desiredForce (using weighting)
                desiredForce = desiredForce.normalized * weighting;
                // SET force to desiredForce - velocity
                force = desiredForce - owner.Velocity;
            }
        }

        return force;
    }
}
