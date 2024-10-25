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
    public float posY;
    [HideInInspector]
    public bool moveY;

    [HideInInspector]
    public float posZ;
    [HideInInspector]
    public bool moveZ;

    bool moveUp;
    bool moveLeft;
    bool moveDown;
    bool moveRight;

    [HideInInspector]
    public float moveSpeed = 5f;

    

    public List<KeyCode> keyUp = new List<KeyCode> { KeyCode.W, KeyCode.UpArrow };
    public List<KeyCode> keyLeft = new List<KeyCode> { KeyCode.A, KeyCode.LeftArrow };
    public List<KeyCode> keyDown = new List<KeyCode> { KeyCode.S, KeyCode.DownArrow };
    public List<KeyCode> keyRight = new List<KeyCode> { KeyCode.D, KeyCode.RightArrow };

    public bool isPlayerOne;
    int playerIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Allow players to move and grab their positions
        moveX = true;
        moveY = true;
        moveZ = true;

        moveUp = true;
        moveLeft = true;
        moveDown = true;
        moveRight = true;

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
        posZ = transform.position.z;

        //Movement listeners
        if (Input.GetKey(keyUp[playerIndex]))
        {
            if (moveZ == true && moveUp == true)
            {
                posZ += moveSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(keyLeft[playerIndex]))
        {
            if (moveX == true && moveLeft == true)
            {
                posX -= moveSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(keyDown[playerIndex]))
        {
            if (moveZ == true && moveDown == true)
            {
                posZ -= moveSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(keyRight[playerIndex]))
        {
            if (moveX == true && moveRight == true)
            {
                posX += moveSpeed * Time.deltaTime;
            }
        }

        transform.position = new Vector3(posX, posY, posZ);
    }

    public void LockUpMovement(bool _lock)
    {
        moveUp = _lock;
    }

    public void LockLeftMovement(bool _lock)
    {
        moveLeft = _lock;
    }

    public void LockDownMovement(bool _lock)
    {
        moveDown = _lock;
    }

    public void LockRightMovement(bool _lock)
    {
        moveRight = _lock;
    }
}
