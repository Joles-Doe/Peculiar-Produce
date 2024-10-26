using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            other.transform.root.GetComponent<PlayerDeath>().Respawn();
        }
    }
}
