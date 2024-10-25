using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables - Adjust value in script
    [HideInInspector]
    public float posX;
    [HideInInspector]
    public bool moveX;

    [HideInInspector]
    public float moveSpeed = 5f;

    [HideInInspector] 
    public float posY;
    [HideInInspector]
    public bool moveY;

    public List<KeyCode> keyLeft = new List<KeyCode> { KeyCode.A, KeyCode.LeftArrow };
    public List<KeyCode> keyRight = new List<KeyCode> { KeyCode.D, KeyCode.RightArrow };

    public bool isPlayerOne;
    int playerIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Allow players to move and grab their positions
        moveX = true;
        moveY = true;

        posX = transform.position.x;
        posY = transform.position.y;
        
        //Sets the key index dependent on if player is player 1
        if (isPlayerOne == true)
        {
            playerIndex = 0;
        }
        else
        {
            playerIndex = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        posX = transform.position.x;
        posY = transform.position.y;

        //Movement listeners
        if (Input.GetKey(keyLeft[playerIndex]))
        {
            if (moveX == true)
            {
                posX += moveSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(keyRight[playerIndex]))
        {
            if (moveX == true)
            {
                posX -= moveSpeed * Time.deltaTime;
            }
        }

        transform.position = new Vector3(posX, posY, -28f);
    }
}
