using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLimiter : MonoBehaviour
{
    //Variables - Drag and drop in editor
    public CameraControl gameCamera;
    public List<Transform> players = new List<Transform>();
    public List<Transform> bounds = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform player in players)
        {
            Vector3 playerPosition = player.position;

            Mathf.Clamp(playerPosition.x, bounds[1].position.x, bounds[0].position.x);
            player.position = playerPosition;
        }

        if (players[0].position.x <= bounds[1].position.x - 5 || players[0].position.x >= bounds[0].position.x - 5 ||
            players[1].position.x <= bounds[1].position.x - 5 || players[1].position.x >= bounds[0].position.x - 5)
        {
            gameCamera.DisableCameraMovement();
        }
        else
        {
            gameCamera.EnableCameraMovement();
        }
    }
}
