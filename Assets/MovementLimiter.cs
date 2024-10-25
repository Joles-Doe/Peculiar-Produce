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
    public List<Transform> boundaries = new List<Transform>();

    Vector3 position;
    Vector3 scale;

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

            //Mathf.Clamp(playerPosition.x, boundary.left, boundary.right);
            //Mathf.Clamp(playerPosition.z, boundary.down, boundary.up);

            //if (playerPosition.x >= boundary.left + 1)
            //{
            //    player.LockLeftMovement(true);
            //}
            //else
            //{
            //    player.LockLeftMovement(false);
            //}

            //if (playerPosition.x <= boundary.right - 1)
            //{
            //    player.LockRightMovement(true);
            //}
            //else
            //{
            //    player.LockRightMovement(false);
            //}

            //if (playerPosition.z <= boundary.up - 1)
            //{
            //    player.LockUpMovement(true);
            //}
            //else
            //{
            //    player.LockUpMovement(false);
            //}

            //if (playerPosition.z >= boundary.down + 1)
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
