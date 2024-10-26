using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementLimiter : MonoBehaviour
{
    //Variables - Drag and drop in editor
    public Camera mainCamera;
    public CameraControl gameCamera;
    public List<PlayerMovement> players = new List<PlayerMovement>();
    public List<Transform> boundaries = new List<Transform>(); // Left Right Up Down

    Vector3 position;
    Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically get the main camera if not assigned
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PlayerMovement player in players)
        {
            //Vector3 playerPosition = player.transform.position;

            //Mathf.Clamp(playerPosition.x, boundaries[0].position.x, boundaries[1].position.x);
            //Mathf.Clamp(playerPosition.z, boundaries[3].position.y, boundaries[2].position.y);

            //player.transform.position = playerPosition;

            //if (playerPosition.x <= boundaries[0].position.x)
            //{
            //    player.LockLeftMovement(true);
            //}
            //else
            //{
            //    player.LockLeftMovement(false);
            //}

            //if (playerPosition.x >= boundaries[1].position.x)
            //{
            //    player.LockRightMovement(true);
            //}
            //else
            //{
            //    player.LockRightMovement(false);
            //}

            //if (playerPosition.z >= boundaries[2].position.y)
            //{
            //    player.LockUpMovement(true);
            //}
            //else
            //{
            //    player.LockUpMovement(false);
            //}

            //if (playerPosition.z <= boundaries[3].position.y)
            //{
            //    player.LockDownMovement(true);
            //}
            //else
            //{
            //    player.LockDownMovement(false);
            //}
        }
    }
}
