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


    // Start is called before the first frame update
    void Start()
    {
        
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
}
