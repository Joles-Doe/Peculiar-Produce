using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHolder : MonoBehaviour
{
    Vector3 respawnLocation;

    public Vector3 GetRespawnLocation()
    {
        return respawnLocation;
    }

    public void SetRespawnLocation(Vector3 _newRespawn)
    {
        respawnLocation = _newRespawn;
    }
}
