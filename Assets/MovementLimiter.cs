using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLimiter : MonoBehaviour
{
    //Variables - Drag and drop in editor
    public CameraControl gameCamera;
    public List<PlayerMovement> players = new List<PlayerMovement>();
    public List<Transform> bounds = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PlayerMovement player in players)
        {
            Vector3 playerPosition = player.transform.position;

            Mathf.Clamp(playerPosition.x, bounds[0].position.x, bounds[1].position.x);
            player.transform.position = playerPosition;

            if (player.transform.position.x <= bounds[0].position.x)
            {
                player.LockLeftMovement(true);
            }
            else
            {
                player.LockLeftMovement(false);
            }
            if (player.transform.position.x >= bounds[1].position.x)
            {
                player.LockRightMovement(true);
            }
            else
            {
                player.LockRightMovement(false);
            }
        }
    }
}
