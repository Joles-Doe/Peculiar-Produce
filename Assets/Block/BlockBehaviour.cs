using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT,
    JUMP,
    THROW,
    STRENGTH
}


public class BlockBehaviour : MonoBehaviour
{

    public BlockType blockType;
    public Rigidbody rb;

    public bool collided;
    public bool picked;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        picked = false;
        collided = false;

    }

    // Update is called once per frame
    void Update()
    {
        
       





    }

  

   











}
