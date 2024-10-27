using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerControl : MonoBehaviour
{
    //[HideInInspector]
    public Vector3 velocity;

    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 3f;
    public float gravity = -25f;
    public float climbSpeed = 5.0f;
    public bool isGrounded = true;
    public bool isClimbing = false;
    public bool canPickup = true;

    public Animator animator;
    public CharacterController controller;
    public AudioSource audioSource;
   
    public SFXManager sfxManager;

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
        audioSource=GetComponent<AudioSource>();

        velocity = Vector3.zero;
        jumpHeight = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        bool isAction1 = isPlayerOne ? Input.GetKey(KeyCode.W) : Input.GetKey(KeyCode.UpArrow);
        bool isAction2 = isPlayerOne ? Input.GetKey(KeyCode.A) : Input.GetKey(KeyCode.LeftArrow);
        bool isAction3 = isPlayerOne ? Input.GetKey(KeyCode.S) : Input.GetKey(KeyCode.DownArrow);
        bool isAction4 = isPlayerOne ? Input.GetKey(KeyCode.D) : Input.GetKey(KeyCode.RightArrow);
        bool isAction5 = isPlayerOne ? Input.GetKey(KeyCode.LeftShift) : Input.GetKey(KeyCode.RightShift);

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
                if (canPickup)
                {
                    if (actionList[throwIndex] != BlockType.NONE)
                    {
                        BlockType lostBlock = actionList[throwIndex];
                        ThrowBlock(lostBlock);
                        actionList[throwIndex] = BlockType.NONE;
                    }
                    else
                    {
                        setBlock(PickupBlock(), throwIndex);
                    }

                    StartCoroutine(PickupWait());

                }
            }
        }

        else
        {
            if (isAction1)
            {
                BlockType block = actionList[0];
                moveDirection += doActionBlock(block);

            }

            if (isAction2)
            {
                BlockType block = actionList[1];
                moveDirection += doActionBlock(block);
            }

            if (isAction3)
            {
                BlockType block = actionList[2];
                moveDirection += doActionBlock(block);
            }

            if (isAction4)
            {
                BlockType block = actionList[3];
                moveDirection += doActionBlock(block);
            }
        }

        moveDirection.Normalize();

        //print(velocity);
        if (!isClimbing)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10f, Mathf.Infinity);

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
        sfxManager.PlayDamageSFX(audioSource, isPlayerOne);
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
    }

    public Vector3 doActionBlock(BlockType type)
    {

        Vector3 moveDirection = Vector3.zero;

        switch (type) {
            case BlockType.UP:
                moveDirection = Vector3.forward;
                sfxManager.PlayStepSFX(audioSource);

                break;
            case BlockType.LEFT:
                moveDirection = Vector3.left;
                sfxManager.PlayStepSFX(audioSource);
                break;
            case BlockType.DOWN:
                moveDirection = Vector3.back;
                sfxManager.PlayStepSFX(audioSource);
                break;
            case BlockType.RIGHT:
                moveDirection = Vector3.right;
                sfxManager.PlayStepSFX(audioSource);
                break;
            case BlockType.JUMP:
                //gravity and jumping
                if (isGrounded) 
                {
                    sfxManager.PlayJumpSFX(audioSource,isPlayerOne);
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

            case BlockType.MAGIC:
                TriggerExplosion(transform.position);
                break;
        }

        return moveDirection;
    }

    public BlockType PickupBlock()
    {
        if (closeBlock != null) 
        {
            sfxManager.PlayPickupSFX(audioSource);
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
            case BlockType.MAGIC:
                prefabname = "magicblock";
                break;
        }

        sfxManager.PlayDropSFX(audioSource);

        GameObject prefab = Resources.Load(prefabname) as GameObject;

        Instantiate(prefab, spawn, Quaternion.identity);
    }

    public IEnumerator JumpWait()
    {
        isGrounded = false;
        yield return new WaitForSeconds(0.8f);
        isGrounded = true;
    }

    public IEnumerator PickupWait()
    {
        canPickup = false;
        yield return new WaitForSeconds(0.5f);
        canPickup = true;
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

    public void TriggerExplosion(Vector3 position)
    {
        GameObject explosionPrefab = Resources.Load("magicsystem") as GameObject;
        // Instantiate the explosion prefab at the specified position and with no rotation
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);

        // Destroy the explosion after 10 seconds
        Destroy(explosion, 10f);
    }



}
