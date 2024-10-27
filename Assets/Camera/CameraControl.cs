using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Variables - Adjust value in script
    public float camOffsetY = 8f; // Fixed vertical offset
    public float baseCamOffsetZ = 9f; // Base distance behind the players
    public float distanceMultiplier = 2f; // Multiplier for dynamic distance based on player spread
    public float zoomOutThreshold = 5f; // Minimum distance between players to trigger zoom out
    public float smoothSpeed = 0.5f; // Smooth transition speed for camera movement

    private void LateUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerCharacters");

        if (players.Length == 0) return; // Exit if there are no players

        // Calculate the average position of all players
        Vector3 averagePosition = Vector3.zero;
        foreach (GameObject player in players)
        {
            averagePosition += player.transform.position;
        }
        averagePosition /= players.Length;

        // Calculate the maximum distance between players
        float maxDistance = 0f;
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = i + 1; j < players.Length; j++)
            {
                float distance = Vector3.Distance(players[i].transform.position, players[j].transform.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }
        }

        // Adjust camOffsetZ based on the maximum distance between players if they exceed the threshold
        float adjustedCamOffsetZ = baseCamOffsetZ;
        if (maxDistance > zoomOutThreshold)
        {
            adjustedCamOffsetZ += (maxDistance - zoomOutThreshold) * distanceMultiplier;
        }

        // Calculate the desired camera position
        Vector3 desiredCameraPosition = averagePosition + new Vector3(0, camOffsetY, -adjustedCamOffsetZ);

        // Smoothly transition to the desired camera position
        transform.position = Vector3.Lerp(transform.position, desiredCameraPosition, smoothSpeed * Time.deltaTime);

        // Optionally, make the camera look at the average position
        transform.LookAt(averagePosition);
    }
}
