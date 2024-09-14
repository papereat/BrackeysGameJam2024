using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public void NewGame()
    {
        GetComponent<SaveManager>().ResetData();
        GetComponent<SceneControler>().LoadScene(1);
    }
    
    public void Continue()
    {
        //GetComponent<SaveManager>().ReadData("save");
        GetComponent<SceneControler>().LoadScene(GetComponent<SaveManager>().saveData.inOverworld ? 2:1);
    }

    public void Settings()
    {
        MainMenu.GetComponent<Canvas>().enabled = !MainMenu.GetComponent<Canvas>().enabled;
        SettingsMenu.GetComponent<Canvas>().enabled = !SettingsMenu.GetComponent<Canvas>().enabled;
    }
    public void Sound()
    {
        
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
