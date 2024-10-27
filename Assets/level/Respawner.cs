using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public CameraControl cam;

    public GameObject mainMenu;

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public GameObject player1;
    public GameObject player2;

    PlayerControl playerScript;
    public SFXManager sfxManager;

    Vector3 spawnLocation;

    public void DestroyAndRespawn(GameObject _player)
    {
        print(_player == player1);
        if (_player == player1)
        {
            spawnLocation = player1.GetComponent<CheckpointHolder>().GetRespawnLocation();
            Destroy(player1);
           
            player1 = Instantiate(player1Prefab, spawnLocation, Quaternion.identity);
            player1.GetComponent<CheckpointHolder>().SetRespawnLocation(spawnLocation);
            playerScript = player1.GetComponentInChildren<PlayerControl>();
            sfxManager.PlayDeathSFX(playerScript.audioSource, playerScript.isPlayerOne);
            playerScript.isPlayerOne = true;
            player1.transform.GetChild(0).tag = "PlayerCharacters";

            Transform playerUI = mainMenu.transform.GetChild(0);
            foreach (Transform child in playerUI.GetComponentsInChildren<Transform>())
            {
                if (child.name != "Player1")
                {
                    child.GetComponent<UiBlocks>().playerControl = playerScript;
                }
            }

            //print(spawnLocation);
            //player1.transform.position = spawnLocation;
            //player1.transform.GetChild(0).transform.position = Vector3.zero;        
            //player1.transform.GetChild(1).transform.position = Vector3.zero;
        }
        
        if (_player == player2)
        {
            spawnLocation = player2.GetComponent<CheckpointHolder>().GetRespawnLocation();
            Destroy(player2);
            player2 = Instantiate(player2Prefab, spawnLocation, Quaternion.identity);
            player2.GetComponent<CheckpointHolder>().SetRespawnLocation(spawnLocation);
            playerScript = player2.GetComponentInChildren<PlayerControl>();
            sfxManager.PlayDeathSFX(playerScript.audioSource, playerScript.isPlayerOne);
            player2.transform.GetChild(0).tag = "PlayerCharacters";

            Transform playerUI = mainMenu.transform.GetChild(1);
            foreach (Transform child in playerUI.GetComponentsInChildren<Transform>())
            {
                if (child.name != "Player2")
                {
                    child.GetComponent<UiBlocks>().playerControl = playerScript;
                }
            }
        }
    }
}
