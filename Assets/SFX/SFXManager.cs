using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public List<AudioClip> footstepSFX = new();


    public List<AudioClip> sJumpSFX = new();
    public List<AudioClip> mJumpSFX = new();

    public AudioClip sdeathSFX;
    public AudioClip mdeathSFX;
    public AudioClip pickupSFX;
    public AudioClip dropSFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
