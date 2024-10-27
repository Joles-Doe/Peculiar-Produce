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
    public AudioClip sCheerSFX;
    public AudioClip mCheerSFX;
    public AudioClip pickupSFX;
    public AudioClip dropSFX;

    public AudioSource source;

    public void PlayDeathSFX(AudioSource _source, bool _playerOne)
    {
        if(_playerOne)
        {
            _source.PlayOneShot(mdeathSFX);
        }
        else
        {
            _source.PlayOneShot(sdeathSFX);
        }
    }

    public void PlayJumpSFX(AudioSource _source, bool _playerOne)
    {
        if(_playerOne)
        {
            
            int choice = Random.Range(1,2);

            
        }
    }


}
