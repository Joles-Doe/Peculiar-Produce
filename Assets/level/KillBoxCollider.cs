using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxCollider : MonoBehaviour
{
    public Respawner respawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") == true)
        {
            respawner.DestroyAndRespawn(other.transform.root.gameObject);
        }
    }
}
