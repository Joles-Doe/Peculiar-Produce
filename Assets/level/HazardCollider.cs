using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Print the name of the object that collided with this object
        Debug.Log("Collided with: " + collision.gameObject.name);
    }
}