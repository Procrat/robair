using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource healthUpSound1;
    public AudioSource healthUpSound2;
    public AudioSource healthDownSound;

    public AudioSource laserSound;

    public AudioSource stabSoundLight;
    public AudioSource stabSoundHeavy;

    public AudioSource drillSoundShort;
    public AudioSource drillSoundMedium;
    public AudioSource drillSoundLong;


    // If you want to test the sounds, uncomment this update method.

    public void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            PlayHealthUpSound1();
        }
        if (Input.GetKeyDown("2"))
        {
            PlayHealthUpSound2();
        }
        if (Input.GetKeyDown("3"))
        {
            PlayHealthDownSound();
        }
        if (Input.GetKeyDown("4"))
        {
            PlayLaserSound();
        }
        if (Input.GetKeyDown("5"))
        {
            PlayStabSoundLight();
        }
        if (Input.GetKeyDown("6"))
        {
            PlayStabSoundHeavy();
        }
        if (Input.GetKeyDown("7"))
        {
            PlayDrillSoundShort();
        }
        if (Input.GetKeyDown("8"))
        {
            PlayDrillSoundMedium();
        }
        if (Input.GetKeyDown("9"))
        {
            PlayDrillSoundLong();
        }
    }

    public void PlayHealthUpSound1()
    {
        healthUpSound1.Play();
    }
    public void PlayHealthUpSound2()
    {
        healthUpSound2.Play();
    }
    public void PlayHealthDownSound()
    {
        healthDownSound.Play();
    }
    public void PlayLaserSound()
    {
        laserSound.Play();
    }
    public void PlayStabSoundLight()
    {
        stabSoundLight.Play();
    }
    public void PlayStabSoundHeavy()
    {
        stabSoundHeavy.Play();
    }
    public void PlayDrillSoundShort()
    {
        drillSoundShort.Play();
    }
    public void PlayDrillSoundMedium()
    {
        drillSoundMedium.Play();
    }
    public void PlayDrillSoundLong()
    {
        drillSoundLong.Play();
    }
}
