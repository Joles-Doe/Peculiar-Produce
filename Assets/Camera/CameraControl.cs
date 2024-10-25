using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //Variables - Drag and drop in editor
    public Transform player1;
    public Transform player2;
    //Variables - Adjust value in script
    [HideInInspector]
    public float offsetY = 20f;
    [HideInInspector]
    public float offsetZ = -12f;
    [HideInInspector]
    public bool canMove = true;

    // Update is called once per frame
    void LateUpdate()
    {
        //Calculate camera midpoint
        Vector3 midPoint = (player1.position + player2.position) / 2;

        //Calculate camera bounds
        float cHeight = Camera.main.orthographicSize * 2;
        float cWidth = cHeight * Camera.main.aspect;

        //Calculate camera limits
        Vector3 minLimit = new Vector3(midPoint.x - cWidth / 2, midPoint.y - cHeight / 2);
        Vector3 maxLimit = new Vector3(midPoint.x + cWidth / 2, midPoint.y + cHeight / 2);

        //Calculate new camera position
        Vector3 targetPosition = new Vector3(midPoint.x, 10, offsetZ);

        //Clamp camera position to keep players in view
        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimit.x, maxLimit.x);
        //targetPosition.y = Mathf.Clamp(targetPosition.y, minLimit.y, maxLimit.y);

        if (canMove == true)
        {
            transform.position = targetPosition;
        }
    }

    public void DisableCameraMovement()
    {
        canMove = false;
    }

    public void EnableCameraMovement()
    {
        canMove = true;
    }
}
