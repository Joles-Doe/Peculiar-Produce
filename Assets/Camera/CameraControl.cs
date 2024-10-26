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
    public bool canMove = true;

    float distance;
    float oldDistance;

    private void Start()
    {
        oldDistance = 15f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Calculate camera midpoint
        Vector3 midPoint = (player1.position + player2.position) / 2;

        distance = Vector3.Distance(player1.position, player2.position);
        

       // print(distance);

        //Calculate camera bounds
        float cHeight = Camera.main.orthographicSize * 2;
        float cWidth = cHeight * Camera.main.aspect;

        //Calculate camera limits
        Vector3 minLimit = new Vector3(midPoint.x - cWidth / 2, midPoint.y - cHeight / 2);
        Vector3 maxLimit = new Vector3(midPoint.x + cWidth / 2, midPoint.y + cHeight / 2);

        //Calculate new camera position
        Vector3 targetPosition = new Vector3(midPoint.x, transform.position.y, midPoint.z - 9f);

        ////Clamp camera position to keep players in view
        //targetPosition.x = Mathf.Clamp(targetPosition.x, minLimit.x, maxLimit.x);
        ////targetPosition.y = Mathf.Clamp(targetPosition.y, minLimit.y, maxLimit.y);

        if (canMove == true)
        {
            transform.position = targetPosition;

            if (distance > oldDistance)
            {
                oldDistance = distance;
                transform.Translate(Vector3.forward * (distance - oldDistance), Space.Self);
                //print(Vector3.forward * (distance - oldDistance));
            }
        }
    }
}
