using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacters") == true)
        {
            collision.transform.root.GetComponentInChildren<PlayerControl>().damaged();
            print("damage");
        }
    }
}