using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public float moveSpeed = 5f;
    [HideInInspector] public float rotationSpeed = 5f;
    [HideInInspector] public float jumpHeight = 0.3f;
    [HideInInspector] public float gravity = -20f;
    public bool isGrounded;

    public PlayerStuffDJ blockParameters;
    
    public List<KeyCode> keyUp = new List<KeyCode> { KeyCode.W, KeyCode.UpArrow };
    public List<KeyCode> keyLeft = new List<KeyCode> { KeyCode.A, KeyCode.LeftArrow };
    public List<KeyCode> keyDown = new List<KeyCode> { KeyCode.S, KeyCode.DownArrow };
    public List<KeyCode> keyRight = new List<KeyCode> { KeyCode.D, KeyCode.RightArrow };
    public List<KeyCode> keyAction = new List<KeyCode> { KeyCode.LeftShift, KeyCode.RightShift };

    public bool isPlayerOne;
    int playerIndex;

    public Animator animator;
    public CharacterController controller;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        //Allow players to move and grab their positions
        moveX = true;
        moveY = true;
        moveZ = true;


       controller = GetComponent<CharacterController>();
      
        
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

        isGrounded = true;
    }

    



    void Update()
    {
        moveDirection = Vector3.zero;


        // Movement listeners
        if (moveX)
        {
            if (Input.GetKey(keyLeft[playerIndex]))
            {
                
                moveDirection = Vector3.left; // Move left


               
            }
            if (Input.GetKey(keyRight[playerIndex]))
            {
               
                moveDirection = Vector3.right; // Move right
            }

           
        }

        if (moveZ)
        {
            if (Input.GetKey(keyUp[playerIndex]))
            {
               
                moveDirection = Vector3.forward; // Move forward
            }
            if (Input.GetKey(keyDown[playerIndex]))
            {
               
                moveDirection = Vector3.back; // Move backward
            }
        }


        controller.Move(moveSpeed * Time.deltaTime * moveDirection);



        //gravity and jumping
        if (isGrounded && blockParameters.GetJump() && Input.GetKeyDown(keyAction[playerIndex]))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -0.8f * gravity);
            StartCoroutine(JumpWait());
            

        }



        velocity.y += gravity * Time.deltaTime;

       

        controller.Move(velocity * Time.deltaTime);


        // Update position
       // Vector3 transformVec = new Vector3(posX, posY, posZ);

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }


        //transform.position = transformVec;


        // Rotate to face the direction of movement
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


      
    }



    public IEnumerator JumpWait()
    {
        isGrounded = false;
        yield return new WaitForSeconds(1.5f);
        isGrounded = true;
    }




}
