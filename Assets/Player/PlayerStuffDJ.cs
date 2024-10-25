using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuffDJ : MonoBehaviour
{
    public bool forward = false;
    public bool back = false;
    public bool right = false;
    public bool left = false;
    
    
    public bool chuck = false;
    public bool jump = false;
    public bool strength = false;

    public List<BlockBehaviour> blocks = new();

    public Rigidbody rb;


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


    }


    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20, LayerMask.GetMask("Block")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Looking at Block");
        }
    }



    public void PickupBlock(BlockBehaviour _block)
    {
        blocks.Add(_block);
    }

   

}
