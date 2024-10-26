using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
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
    public bool isDead;

    [HideInInspector]
    public Vector3 velocity;

    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -2f;
    public bool isGrounded;

    public Animator animator;
    public CharacterController controller;


    //active block is the last block in the list
    public BlockInventory inventory = new BlockInventory();
    public bool isPlayerOne = false;

    public List<GameObject> blockPrefabs;
    RaycastHit hit;
    BlockBehaviour closeBlock;

   



    // Start is called before the first frame update
    void Start()
    {
        moveX = true;
        moveY = true;
        moveZ = true;

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        bool isAction1 = isPlayerOne ? Input.GetKeyDown(KeyCode.W) : Input.GetKeyDown(KeyCode.UpArrow);
        bool isAction2 = isPlayerOne ? Input.GetKeyDown(KeyCode.A) : Input.GetKeyDown(KeyCode.LeftArrow);
        bool isAction3 = isPlayerOne ? Input.GetKeyDown(KeyCode.S) : Input.GetKeyDown(KeyCode.DownArrow);
        bool isAction4 = isPlayerOne ? Input.GetKeyDown(KeyCode.D) : Input.GetKeyDown(KeyCode.RightArrow);
        bool isAction5 = isPlayerOne ? Input.GetKeyDown(KeyCode.LeftShift) : Input.GetKeyDown(KeyCode.RightShift);

        // If wanting to throw the block
        if (isAction5)
        {
            int throwIndex = 0;

            if (isAction1)
            {
                throwIndex = 0;
            }
            else if (isAction2)
            {
                throwIndex = 1;
            }
            else if (isAction3)
            {
                throwIndex = 2;
            }
            else if (isAction4)
            {
                throwIndex = 3;
            }

            if (inventory.at(throwIndex) != BlockType.NONE)
            {
                BlockType lostBlock = inventory.at(throwIndex);
                ThrowBlock(lostBlock);
                inventory.removeAt(throwIndex);
            }
        }

        else
        {
            if (isAction1)
            {
                BlockType block = inventory.at(0);
                if (block == BlockType.NONE)
                {
                    inventory.addBlock(PickupBlock(), 0);
                }
                else
                {
                    doActionBlock(block);
                }
            }

            if (isAction2)
            {
                BlockType block = inventory.at(1);
                if (block == BlockType.NONE)
                {
                    inventory.addBlock(PickupBlock(), 1);
                }
                else
                {
                    doActionBlock(block);
                }
            }

            if (isAction3)
            {
                BlockType block = inventory.at(2);
                if (block == BlockType.NONE)
                {
                    inventory.addBlock(PickupBlock(), 2);
                }
                else
                {
                    doActionBlock(block);
                }
            }

            if (isAction4)
            {
                BlockType block = inventory.at(3);
                if (block == BlockType.NONE)
                {
                    inventory.addBlock(PickupBlock(), 3);
                }
                else
                {
                    doActionBlock(block);
                }
            }
        }
    }


    public void damaged()
    {
        BlockType lostBlock = inventory.loseBlock();
        ThrowBlock(lostBlock);
    }


    private void FixedUpdate()
    {
        Vector3 offset = new(0,1,0);

        if (Physics.Raycast(transform.position + offset, transform.TransformDirection(Vector3.forward), out hit, 50, LayerMask.GetMask("Block")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            closeBlock = hit.collider.gameObject.GetComponent<BlockBehaviour>();
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
        }
    }

    public void doActionBlock(BlockType type)
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;

        Vector3 moveDirection = Vector3.zero;
        bool isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            velocity.y = 0f;
        }

        switch (type) {
            case BlockType.UP:
                break;
            case BlockType.LEFT:
                break;
            case BlockType.DOWN:
                break;
            case BlockType.RIGHT:
                break;
            case BlockType.JUMP:
                if (isGrounded && blockParameters.GetJump() && Input.GetKeyDown(keyAction[playerIndex]))
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                }

            case BlockType.CLIMB:
                break;
        }

        Vector3 transformVec = new Vector3(posX, posY, posZ);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        controller.Move(moveSpeed * Time.deltaTime * moveDirection);

        if (moveDirection != Vector3.zero) {
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }

        transform.position = transformVec;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public BlockType PickupBlock()
    {
        BlockType blockType = closeBlock.blockType;
        Destroy(closeBlock);
        return blockType;
    }

    public void ThrowBlock(BlockType blockType)
    {
        GameObject selectedBlockPrefab = null;

        // Assuming blockPrefabs is an array or list of GameObjects that includes a BlockBehaviour component
        foreach (GameObject blockPrefab in blockPrefabs)
        {
            BlockBehaviour blockBehaviour = blockPrefab.GetComponent<BlockBehaviour>();
            if (blockBehaviour != null && blockType == blockBehaviour.blockType)
            {
                selectedBlockPrefab = blockPrefab;
                break;
            }
        }

        // Check if a matching block prefab was found
        if (selectedBlockPrefab != null)
        {
            // Instantiate the block prefab at the player's position + some offset
            GameObject thrownBlock = Instantiate(selectedBlockPrefab, transform.position + transform.forward, Quaternion.identity);

            // Add a Rigidbody component to the thrown block if it doesn't already have one
            Rigidbody rb = thrownBlock.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply force to the block in the direction the player is facing
                rb.AddForce(transform.forward, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("No matching block prefab found for block type: " + blockType);
        }
    }
}
