using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum BlockType
{
    UP,
    LEFT,
    DOWN,
    RIGHT,
    JUMP,
    CLIMB,
    NONE
}


/*
 * W, A, S, D, LSHIFT
 * UP, LEFT, DOWN, RIGHT, RSHIFT
 */

public class BlockInventory : MonoBehaviour
{

    public List<BlockType> blocks;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
       

    }

    public void addBlock(BlockType block, int id)
    {

    }

    public void removeAt(int index) {
        blocks[index] = BlockType.NONE;
    }

    public BlockType at(int index) {
        return blocks[index];
    }

    public BlockType loseBlock()
    {
        int id = Random.Range(0, blocks.Count);
        BlockType block = blocks[Random.Range(0, blocks.Count)];
        blocks[id] = BlockType.NONE;
        return block;
    }


}
