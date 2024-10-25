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
    public Material material;


    // Start is called before the first frame update
    void Start()
    {

        //refernce to material
        material = GetComponent<Renderer>().material;
        
        




    }

    // Update is called once per frame
    void Update()
    {
        






    }

    
  








}
