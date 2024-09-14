using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundController : MonoBehaviour
{
    public static SoundController soundController;
    public AudioSource overSoundSrc;
    public AudioSource underSoundSrc;
    public AudioSource boatSoundSrc;
    public AudioSource musicSrc;
    public AudioClip[] sounds;

    public float settingVolume;
    public float musicVolume;
    public float playerOverStateVolume;
    public float playerUnderStateVolume;
    public float boatStateVolume;

    void Start()
    {
    }
    void Awake()
    {
        soundController = this;
    }
    void Update()
    {
        overSoundSrc.volume = settingVolume * playerOverStateVolume;
        underSoundSrc.volume = settingVolume * playerUnderStateVolume;
        boatSoundSrc.volume = settingVolume * boatStateVolume;
        musicSrc.volume = musicVolume;
    }

    public void playOverSound(int soundIndex, float soundVolume)
    {
        
        overSoundSrc.PlayOneShot(sounds[soundIndex], soundVolume);
    }

    public void playUnderSound(int soundIndex, float soundVolume)
    {
        
        underSoundSrc.PlayOneShot(sounds[soundIndex], soundVolume);
    }
    public void playBoatSound(int soundIndex, float soundVolume)
    {
        
        boatSoundSrc.PlayOneShot(sounds[soundIndex], soundVolume);
    }
    
    public void playMusic(int soundIndex)
    {
        musicSrc.PlayOneShot(sounds[soundIndex]);
    }


}
