using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public void PlayBrickSlider()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayBreakout()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayPong2D()
    {
        SceneManager.LoadScene(3);
    }
    public void PlayPong3D()
    {
        SceneManager.LoadScene(4);
    }
    public void PlayMemory()
    {
        SceneManager.LoadScene(5);
    }
    public void PlayWheresWally()
    {
        SceneManager.LoadScene(6);
    }
}
