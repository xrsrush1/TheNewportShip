using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickupPhysics : MonoBehaviour
{
    public void ObjectPickedUp()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
        gameObject.GetComponent<Rigidbody>().useGravity = false; // Disable gravity
    }
    public void ObjectReleased()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics
        gameObject.GetComponent<Rigidbody>().useGravity = true; // Re-enable gravity
    }
}
