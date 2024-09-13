using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
    public static SceneControler sc;

    void Awake()
    {
        sc = this;
    }

    public void LoadScene(int moveTo)
    {
        SceneManager.LoadScene(moveTo);
    }
}
