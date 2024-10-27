using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public List<AudioClip> footstepSFX = new();


    public List<AudioClip> sJumpSFX = new();
    public List<AudioClip> mJumpSFX = new();

    public AudioClip sDamageSFX;
    public AudioClip sdeathSFX;

    public AudioClip mDamageSFX;
    public AudioClip mdeathSFX;

    public AudioClip sCheerSFX;
    public AudioClip mCheerSFX;
    
    public AudioClip pickupSFX;
    public AudioClip dropSFX;

    public void PlayDamageSFX(AudioSource _source, bool _playerOne)
    {
        if (_playerOne)
        {
            _source.clip = mDamageSFX;
        }
        else
        {
            _source.clip = sDamageSFX;
        }
        _source.Play();

    }


    public void PlayDeathSFX(AudioSource _source, bool _playerOne)
    {
        if(_playerOne)
        {
            _source.clip = mdeathSFX;
        }
        else
        {
            _source.clip = sdeathSFX;
        }

        _source.Play();
    }

    public void PlayJumpSFX(AudioSource _source, bool _playerOne)
    {
        if(_playerOne)
        {
            
            int choice = Random.Range(0,2);

            _source.PlayOneShot( mJumpSFX[choice]);
            
            
        }
        else
        {
            int choice = Random.Range(0,3);
            _source.PlayOneShot(sJumpSFX[choice]);
            
        }

        _source.Play();
    }

    public void PlayCheerSFX(AudioSource _source, bool _playerOne)
    {
        if( _playerOne)
        {
            _source.clip = mCheerSFX;
            
        }
        else
        {
            _source.clip= sCheerSFX;
        }
        _source.Play();

    }

    public void PlayStepSFX(AudioSource _source)
    {
        int choice = Random.Range(0, footstepSFX.Count);

        _source.clip = footstepSFX[choice];
        _source.Play();

    }

    public void PlayPickupSFX(AudioSource _source)
    {
        _source.clip = pickupSFX;
        _source.Play();
    }

    public void PlayDropSFX(AudioSource _source)
    {
        _source.clip = dropSFX;
        _source.Play();
    }


}
