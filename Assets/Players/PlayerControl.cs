using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public Vector3 velocity;

    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 10f;
    public float gravity = -25f;
    public float climbSpeed = 5.0f;
    public bool isGrounded = true;
    public bool isClimbing = false;

    public Animator animator;
    public CharacterController controller;


    public bool isPlayerOne = false;

    public List<GameObject> blockPrefabs;
    RaycastHit hit;
    BlockBehaviour closeBlock;

    public List<BlockType> actionList;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        bool isAction1 = isPlayerOne ? Input.GetKey(KeyCode.W) : Input.GetKey(KeyCode.UpArrow);
        bool isAction2 = isPlayerOne ? Input.GetKey(KeyCode.A) : Input.GetKey(KeyCode.LeftArrow);
        bool isAction3 = isPlayerOne ? Input.GetKey(KeyCode.S) : Input.GetKey(KeyCode.DownArrow);
        bool isAction4 = isPlayerOne ? Input.GetKey(KeyCode.D) : Input.GetKey(KeyCode.RightArrow);
        bool isAction5 = isPlayerOne ? Input.GetKey(KeyCode.LeftShift) : Input.GetKey(KeyCode.RightShift);
        bool isAction6 = isPlayerOne ? Input.GetKey(KeyCode.LeftControl) : Input.GetKey(KeyCode.RightControl);

        Vector3 moveDirection = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, LayerMask.GetMask("Climb")))
        {
            isClimbing = true;
        } else
        {
            isClimbing = false;
        }

        // If wanting to throw the block
        if (isAction5)
        {
            int throwIndex = -1;

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

            if (throwIndex >= 0)
            {
                if (actionList[throwIndex] != BlockType.NONE)
                {
                    BlockType lostBlock = actionList[throwIndex];
                    ThrowBlock(lostBlock);
                    actionList[throwIndex] = BlockType.NONE;
                }
            }
        }

        else
        {
            if (isAction1)
            {
                BlockType block = actionList[0];
                if (block == BlockType.NONE)
                {
                    setBlock(PickupBlock(), 0);
                }
                else
                {
                    moveDirection += doActionBlock(block);
                }
            }

            if (isAction2)
            {
                BlockType block = actionList[1];
                if (block == BlockType.NONE)
                {
                    setBlock(PickupBlock(), 1);
                }
                else
                {
                    moveDirection += doActionBlock(block);
                }
            }

            if (isAction3)
            {
                BlockType block = actionList[2];
                if (block == BlockType.NONE)
                {
                    setBlock(PickupBlock(), 2);
                }
                else
                {
                    moveDirection += doActionBlock(block);
                }
            }

            if (isAction4)
            {
                BlockType block = actionList[3];
                if (block == BlockType.NONE)
                {
                    setBlock(PickupBlock(), 3);
                }
                else
                {
                    moveDirection += doActionBlock(block);
                }
            }

            if (isAction6)
            {
                BlockType block = actionList[4];
                moveDirection += doActionBlock(block);

            }
        }

        moveDirection.Normalize();

        //print(velocity);
        if (!isClimbing)
        {

            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        controller.Move(moveSpeed * Time.deltaTime * moveDirection);




        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


    public void damaged()
    {
        BlockType lostBlock = loseBlock();
        ThrowBlock(lostBlock);
    }


    private void FixedUpdate()
    {
        float maxDistance = 1.75f; // Set your maximum distance here
        closeBlock = null; // Reset the closeBlock

        // Find all blocks in the scene. Adjust the tag to match your setup.
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        float closestDistance = maxDistance; // Initialize with the max distance
        float distance;

        foreach (GameObject block in allBlocks)
        {
            distance = Vector3.Distance(transform.position, block.transform.position);

            // Check if this block is the closest and within the max distance
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closeBlock = block.GetComponent<BlockBehaviour>();
            }
        }

        // If closeBlock is still null, no blocks were found within the max distance
        if (closeBlock == null)
        {
            //Debug.Log("No blocks within the specified distance.");
        }
        else
        {
           // Debug.Log("Closest block found at distance: " + closestDistance);
        }



    }

    public Vector3 doActionBlock(BlockType type)
    {

        Vector3 moveDirection = Vector3.zero;

        switch (type) {
            case BlockType.UP:
                moveDirection = Vector3.forward;
                break;
            case BlockType.LEFT:
                moveDirection = Vector3.left;
                break;
            case BlockType.DOWN:
                moveDirection = Vector3.back;
                break;
            case BlockType.RIGHT:
                moveDirection = Vector3.right;
                break;
            case BlockType.JUMP:
                //gravity and jumping
                if (isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -0.8f * gravity);
                    StartCoroutine(JumpWait());
                }
                break;

            case BlockType.CLIMB:
                if (isClimbing)
                {
                    velocity.y += climbSpeed * Time.deltaTime;
                }
                break;
        }

        return moveDirection;
    }

    public BlockType PickupBlock()
    {
        if (closeBlock != null) {
            BlockType blockType = closeBlock.blockType;
            Destroy(closeBlock.gameObject);
            return blockType;
        }
        return BlockType.NONE; 
    }

    public void ThrowBlock(BlockType _blockType)
    {
        Vector3 height_offset = new Vector3(0, 10, 0);

        Vector3 spawn = transform.position + height_offset;


        string prefabname = "jumpblock";

        switch (_blockType)
        {
            case BlockType.UP:
                prefabname = "upblock";
                break;
            case BlockType.LEFT:
                prefabname = "leftblock";
                break;
            case BlockType.DOWN:
                prefabname = "downblock";
                break;
            case BlockType.RIGHT:
                prefabname = "rightblock";
                break;
            case BlockType.JUMP:
                prefabname = "jumpblock";
                break;
            case BlockType.CLIMB:
                prefabname = "climbblock";
                break;
        }

        GameObject prefab = Resources.Load(prefabname) as GameObject;

        Instantiate(prefab, spawn, Quaternion.identity);
    }

    public IEnumerator JumpWait()
    {
        isGrounded = false;
        yield return new WaitForSeconds(2.0f);
        isGrounded = true;
    }

    public void setBlock(BlockType block, int id)
    {
        actionList[id] = block;
    }

    public BlockType loseBlock()
    {
        int id = Random.Range(0, actionList.Count);
        BlockType block = actionList[Random.Range(0, actionList.Count)];
        actionList[id] = BlockType.NONE;
        return block;
    }
}
