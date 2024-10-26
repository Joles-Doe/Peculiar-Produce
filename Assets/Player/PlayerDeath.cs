using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public CharacterController player;
    public Transform playerRagdoll;
    public CheckpointHolder checkpoint;

    private void FixedUpdate()
    {
        print(player.transform.position.y);
        if (player.transform.position.y <= -10)
        {
            Respawn();
        }
    }



    public void Respawn()
    {
        print("Respawn");
        print(checkpoint.GetRespawnLocation());
        player.transform.position = checkpoint.GetRespawnLocation();
        player.velocity.Set(0, 0, 0);
        playerRagdoll.position = checkpoint.GetRespawnLocation();
    }
}
