using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour {

    public void Start()
    {
        PlayerPrefs.SetInt("difficulty", 3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
