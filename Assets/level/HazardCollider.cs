using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCharacters") == true)
        {
            other.transform.root.GetComponentInChildren<PlayerControl>().damaged();
            print("damage");
        }
    }
}