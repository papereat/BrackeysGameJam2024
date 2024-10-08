using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static SettingsController settingsController;
    SoundController soundController;
    public GameObject SettingsMenu;

    public Slider soundSlider;
    public Slider musicSlider;
    public KeyCode settingsKey = KeyCode.Escape;
    void Awake()
    {
        settingsController = this;
    }
    void Start()
    {
        soundController = SoundController.soundController;
    }
    void Update()
    {
        if(Input.GetKeyDown(settingsKey))
        {
            soundController.playOverSound(11, 1);
            SettingsMenu.SetActive(!SettingsMenu.activeSelf);
        }
    }
    public void SettingsNBack()
    {
        soundController.playOverSound(11, 1);

        Debug.Log("workeD");
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
    }
    public float SoundVolume()
    {
        return soundSlider.value;
    }
    public float MusicVolume()
    {
        return musicSlider.value;
    }

    public void SoundVolume(float input)
    {
        soundSlider.value = input;
    }
    public void MusicVolume(float input)
    {        
        musicSlider.value = input;

    }
}
