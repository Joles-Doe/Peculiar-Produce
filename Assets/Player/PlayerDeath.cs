using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Transform parent;
    public CheckpointHolder checkpoint;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();    
    }

    public void Respawn()
    {
        parent.position = checkpoint.GetRespawnLocation();
    }
}
