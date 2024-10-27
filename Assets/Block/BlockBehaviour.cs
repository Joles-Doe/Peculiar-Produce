using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    UP,
    LEFT,
    DOWN,
    RIGHT,
    JUMP,
    CLIMB,
    MAGIC,
    NONE
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
        collided = false;
        picked = false;

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SetCollided(bool _collided)
    {
        collided= _collided;
    }

    public void SetPicked(bool _picked)
    {
        picked= _picked;
    }

    public bool GetPicked() { return picked; }

    public bool GetCollided() { return collided; }








}
