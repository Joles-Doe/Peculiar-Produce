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

    public bool collided = false;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

        
        




    }

    // Update is called once per frame
    void Update()
    {
        
       





    }

  

    private void OnCollisionEnter(Collision collision)
    {

        if(!collided)
        {
            rb.AddExplosionForce(2, transform.position, 1,50,ForceMode.Impulse);
        }
        
    
    }

    private void OnCollisionExit(Collision collision)
    {
        collided = true;
    }











}
