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
    public float camOffsetZ = 9f;
    float minZ = 0f;
    List<float> zTargets = new List<float> { 0, 0 };

    float minY = 0f;
    float currentY = 0f;
    List<float> yTargets = new List<float> { 0, 0 };

    int targetIndex = 0;

    Vector3 midPoint;

    bool lerpMove = false;
    bool lerpInverse = false;
    float ratio;

    bool targetNull = false;
    float distance;

    private void Start()
    {
        currentY = transform.position.y;

        //Set targets
        zTargets[0] = camOffsetZ;
        zTargets[1] = camOffsetZ + 5;

        yTargets[0] = currentY;
        yTargets[1] = currentY + 5;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player1 == null || player2 == null)
        {
            if (player1 == null)
            {
                midPoint = player2.position;
            }
            else if (player2 == null)
            {
                midPoint = player1.position;
            }
        }
        else
        {
            distance = Vector3.Distance(player1.position, player2.position);

            if (distance > 12)
            {
                //Check if camera should be zooming in
                if (lerpInverse == false)
                {
                    lerpInverse = true;
                    targetIndex = 1;

                    minZ = camOffsetZ;
                    minY = transform.position.y;

                    lerpMove = true;
                    ratio = 0;
                }
            }
            else
            {
                //Check if camera should be zooming in
                if (lerpInverse == true)
                {
                    lerpInverse = false;
                    targetIndex = 0;

                    minZ = camOffsetZ;
                    minY = transform.position.y;

                    lerpMove = true;
                    ratio = 0;
                }
            }

            if (lerpMove == true)
            {
                //Lerp Y and Z values
                currentY = Mathf.Lerp(minY, yTargets[targetIndex], ratio);
                camOffsetZ = Mathf.Lerp(minZ, zTargets[targetIndex], ratio);

                ratio += Time.deltaTime;

                if (currentY == yTargets[targetIndex] && camOffsetZ == zTargets[targetIndex])
                {
                    lerpMove = false;
                }
            }

            //Calculate camera midpoint
            midPoint = (player1.position + player2.position) / 2;
        }
        transform.position = new Vector3(midPoint.x, midPoint.y + currentY - 3, midPoint.z - camOffsetZ);
    }
}
