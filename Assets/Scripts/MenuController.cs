using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Letter;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;
    
    SoundController soundController;

    public void Start()
    {
        soundController = SoundController.soundController;
    }
    public void NewGame()
    {
        soundController.playOverSound(11, 1);

        MainMenu.GetComponent<Canvas>().enabled = !MainMenu.GetComponent<Canvas>().enabled;
        Letter.GetComponent<Canvas>().enabled = !Letter.GetComponent<Canvas>().enabled;
    }
    public void Next()
    {
        soundController.playOverSound(11, 1);

        GetComponent<SaveManager>().ResetData();
        GetComponent<SceneControler>().LoadScene(1);
    }    
    public void Continue()
    {
        soundController.playOverSound(11, 1);

        //GetComponent<SaveManager>().ReadData("save");
        GetComponent<SceneControler>().LoadScene(GetComponent<SaveManager>().saveData.inOverworld ? 2:1);
    }

    public void Settings()
    {
        soundController.playOverSound(11, 1);

        MainMenu.GetComponent<Canvas>().enabled = !MainMenu.GetComponent<Canvas>().enabled;
    }
    public void Credits()
    {
        soundController.playOverSound(11, 1);

        CreditsMenu.GetComponent<Canvas>().enabled = !CreditsMenu.GetComponent<Canvas>().enabled;
        MainMenu.GetComponent<Canvas>().enabled = !MainMenu.GetComponent<Canvas>().enabled;
    }


    public void QuitGame()
    {
        soundController.playOverSound(11, 1);

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
