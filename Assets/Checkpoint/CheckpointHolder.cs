using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHolder : MonoBehaviour
{
    Vector3 respawnLocation;

    public void ChangeRespawnLocation(Vector3 _newRespawn)
    {
        respawnLocation = _newRespawn;
    }

    public Vector3 GetRespawnLocation()
    {
        return respawnLocation;
    }
}
