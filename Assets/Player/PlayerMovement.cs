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

    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    
    public List<KeyCode> keyUp = new List<KeyCode> { KeyCode.W, KeyCode.UpArrow };
    public List<KeyCode> keyLeft = new List<KeyCode> { KeyCode.A, KeyCode.LeftArrow };
    public List<KeyCode> keyDown = new List<KeyCode> { KeyCode.S, KeyCode.DownArrow };
    public List<KeyCode> keyRight = new List<KeyCode> { KeyCode.D, KeyCode.RightArrow };

    public bool isPlayerOne;
    int playerIndex;

    public Animator animator;

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

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;

        Vector3 moveDirection = Vector3.zero;

        // Movement listeners
        if (moveX)
        {
            if (Input.GetKey(keyLeft[playerIndex]))
            {
                posX -= moveSpeed * Time.deltaTime;
                moveDirection += Vector3.left; // Move left
            }
            if (Input.GetKey(keyRight[playerIndex]))
            {
                posX += moveSpeed * Time.deltaTime;
                moveDirection += Vector3.right; // Move right
            }
        }

        if (moveZ)
        {
            if (Input.GetKey(keyUp[playerIndex]))
            {
                posZ += moveSpeed * Time.deltaTime;
                moveDirection += Vector3.forward; // Move forward
            }
            if (Input.GetKey(keyDown[playerIndex]))
            {
                posZ -= moveSpeed * Time.deltaTime;
                moveDirection += Vector3.back; // Move backward
            }
        }

        // Update position
        Vector3 transformVec = new Vector3(posX, posY, posZ);

        if (transformVec - transform.position != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }


        transform.position = transformVec;


        // Rotate to face the direction of movement
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
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
