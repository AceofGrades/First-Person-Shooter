using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    #region Variables
    public float maxVelocity = 5f, maxDistance = 5f;
    protected NavMeshAgent agent;
    protected SteeringBehaviour[] behaviours;
    protected Vector3 velocity;
    #endregion
    // Property
    public Vector3 Velocity
    {
        protected set { velocity = value; }
        get { return velocity; }
    }

    void Awake()
    {
        // Get Nav Component
        agent = GetComponent<NavMeshAgent>();
        // Get all SteeringBehaviours on AI
        behaviours = GetComponents<SteeringBehaviour>();
    }

    private void Update()
    {
        CalculateForce();
    }

    // Calculate all forces from all behaviours
    public virtual Vector3 CalculateForce()
    {
        // Pseudocode
        // Step 1: Create result Vector3
        // SET force to zero
        Vector3 force = Vector3.zero;

        // Step 2: Loop through all behviours and get forces
        foreach (var behaviour in behaviours)
        {
            // Apply force to behaviour.GetForce x Weighting
            force += behaviour.GetForce() * behaviour.weighting;

            // Step 3: Limit the total force to max speed
            // If force magnitude > maxVelocity
            if (force.magnitude > maxVelocity)
            {
                // Set force to force normalized x maxSpeed
                force = force.normalized * maxVelocity;

                // Break - Exits the loop
                break;
            }
        }


        // Step 4: Limit total velocity to max velocity if it exceeds
        velocity += force.normalized * Time.deltaTime;
        // IF velociy magnitdue > max velocity
        if(velocity.magnitude > maxVelocity)
        {
            // Set velocity to velocity normalised x max velocity
            velocity = velocity.normalized * maxVelocity;
        }

        // Step 5: Sample destination for NavMeshAgent
        // If velocity magnitude > 0(velocity not zero)
        if (velocity.magnitude > 0)
        {
            // Set pos to current (position) + velocity x delta
            Vector3 pos = transform.position + velocity * Time.deltaTime;
            NavMeshHit hit;
            // If NavMesh SamplePosition within NavMesh
            if (NavMesh.SamplePosition(pos, out hit, maxDistance, -1))
            {
                // Set agent destination to git position
                agent.SetDestination(hit.position);
            }
        }

        // Step 6: Return force
        return force;
    }
}
