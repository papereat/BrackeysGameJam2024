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

    public float soundSettingsVolume = 1f;
    public float musicSettingsVolume = 1f;
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
        overSoundSrc.volume = soundSettingsVolume * playerOverStateVolume;
        underSoundSrc.volume = soundSettingsVolume * playerUnderStateVolume;
        boatSoundSrc.volume = soundSettingsVolume * boatStateVolume;
        musicSrc.volume = musicSettingsVolume;
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
    
    public void playMusic(int soundIndex, float muiscVolume)
    {
        musicSrc.PlayOneShot(sounds[soundIndex], muiscVolume);
    }


}
