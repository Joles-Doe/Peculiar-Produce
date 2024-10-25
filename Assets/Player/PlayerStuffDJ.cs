using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuffDJ : MonoBehaviour
{

    //movement
    public bool forward = false;
    public bool back = false;
    public bool right = false;
    public bool left = false;
    
    
    //abilities
    public bool chuck = false;
    public bool jump = false;
    public bool strength = false;

    

    public bool pickup = false;


    //active block is the last block in the list
    public List<BlockBehaviour> blocks = new();

    public Rigidbody rb;

    RaycastHit hit;

    public BlockBehaviour targetedBlock;

    public float horizontalMovement;
    public float verticalMovement;

    public float speed = 10;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        foreach(BlockBehaviour block in blocks) 
        {

            if(block != null)
            {
                if(block.blockType == BlockType.FORWARD)
                {
                    forward = true;
                }
                if (block.blockType == BlockType.BACK)
                {
                    back = true;
                }
                if (block.blockType == BlockType.LEFT)
                {
                    left = true;
                }
                if (block.blockType == BlockType.RIGHT)
                {
                    forward = true;
                }
                if(block.blockType == BlockType.JUMP)
                {
                    jump = true;
                }
                if (block.blockType == BlockType.THROW)
                {
                    chuck = true;
                }
                if(block.blockType == BlockType.STRENGTH)
                {
                    strength = true;
                }


            }


        }

        if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Hash))
        {
            //a block can be picked up, theres a visible block and there is space in the list
            if(pickup && targetedBlock != null && blocks.Count < 5)
            {
                PickupBlock(targetedBlock);
                
            }
        }

        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Slash))
        {
            if(jump || chuck || strength)
            {
                ThrowBlock();
            }
        }
        


    }


    private void FixedUpdate()
    {
       
       
        if(forward && verticalMovement > 0)
        {
            rb.velocity = Vector3.forward * speed;
        }
        
        if(back && verticalMovement < 0)
        {
            rb.velocity = Vector3.back * speed; 
        }

        if(right && horizontalMovement > 0)
        {
            rb.velocity = Vector3.right * speed;
        }

        if(left && verticalMovement < 0)
        {
            rb.velocity = Vector3.left * speed;
        }

        if(jump && Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            //change this to stop it from flying
            rb.velocity = Vector3.up * speed;
        }

        //raycast for block collision

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20, LayerMask.GetMask("Block")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Looking at Block");


            //stores block the player is looking at
            targetedBlock = hit.collider.gameObject.GetComponent<BlockBehaviour>();

            //that block can be picked up
            pickup = true;
        }
        else
        {
            targetedBlock = null;
            pickup = false;
        }
    }



    public void PickupBlock(BlockBehaviour _block)
    {
        //TODO: Link block to player's hand


        blocks.Add(_block);
    }

    public void ThrowBlock()
    {
        //lets it bounce
        blocks[^1].collided = false;

        //remove from player abilities
        blocks.Remove(blocks[^1]);
        
    }

   

}
