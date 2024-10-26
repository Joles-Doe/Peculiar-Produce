using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuffDJ : MonoBehaviour
{

    //movement
    //public bool forward = false;
    //public bool back = false;
    //public bool right = false;
    //public bool left = false;
    
    
    //abilities
    public bool chuck = false;
    public bool jump = false;
    public bool strength = false;

    

    public bool pickup = false;


    //active block is the last block in the list
    public List<BlockBehaviour> blocks = new();

    


    RaycastHit hit;
    public BlockBehaviour targetedBlock;

    public bool isGrounded = true;

    Vector3 blockOffset;


    //Give the player a gameobject that follows their hand
    //public transform playerHand;



    // Start is called before the first frame update
    void Start()
    {
        
        blockOffset = new Vector3(0, 1, 0);


    }

    // Update is called once per frame
    void Update()
    {


        foreach (BlockBehaviour block in blocks)
        {

            if (block.blockType == BlockType.JUMP)
            {
                jump = true;
            }
          

            if (block.blockType == BlockType.THROW)
            {
                chuck = true;
            }
           

            if (block.blockType == BlockType.STRENGTH)
            {
                strength = true;
            }
          




        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Hash))
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

        //keep player hand on the player's body
        //playerHand.position = Vector3.zero;
       


    }


    private void FixedUpdate()
    {
       
       
      

        if (jump && isGrounded && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
          
            isGrounded = false;

        }

        Vector3 offset = new(0,1,0);

        //raycast for block collision

        if (Physics.Raycast(transform.position + offset, transform.TransformDirection(Vector3.forward), out hit, 50, LayerMask.GetMask("Block")))
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
           
           
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
           // Debug.Log("Did not Hit");
            
            targetedBlock = null;
            pickup = false;
        }




        //stack picked up blocks onto character
        for (int i = 0; i < blocks.Count - 1; i++)
        {
            blocks[i].rb.Move(transform.position + blockOffset * (i + 1), Quaternion.identity);
        }

       // blocks[^1].rb.Move(playerHand.position);



    }



    public void PickupBlock(BlockBehaviour _block)
    {

        if (!_block.GetPicked())
        {
            

            blocks.Add(_block);
            _block.SetPicked(true);
        }
    }

    public void ThrowBlock()
    {
        if (blocks.Count != 0)
        {
            //lets it bounce
            blocks[^1].SetCollided( false);
            blocks[^1].SetPicked(false);

            if (blocks[^1].blockType == BlockType.JUMP)
            {
                jump = false;
            }


            if (blocks[^1].blockType == BlockType.THROW)
            {
                chuck = false;
            }


            if (blocks[^1].blockType == BlockType.STRENGTH)
            {
                strength = false;
            }
            //remove from player abilities
            blocks.Remove(blocks[^1]);


        }

    }

 

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

    }

    
   

}
