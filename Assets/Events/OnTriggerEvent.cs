using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    // Reference Tag to detect collisions with
    public string hitTag;
    public UnityEvent onEnter;

    private void OnTriggerEnter(Collider other)
    {
        // If hitting hit Tag
        if(other.gameObject.tag == "Player")
        {
            // Invoke (Run) the event!
            onEnter.Invoke();
        }
    }
}
