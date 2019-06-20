using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BS_PauseMenu : MonoBehaviour {

    // Public variables
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public TMP_Text score;

	/// <summary>
    /// On game update check if player is trying to pause game
    /// </summary>
	void Update () {
        if (Input.GetKeyDown(KeyCode.P) || 
            Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    /// <summary>
    /// Resume game when paused
    /// </summary>
    public void Resume()
    {
        // Deactivate pause menu
        pauseMenuUI.SetActive(false);
        foreach (GameObject menu in GameObject.FindGameObjectsWithTag("menu"))
        {
            menu.SetActive(false);
        }
        // Turn on score
        score.gameObject.SetActive(true);

        // Activate game
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /// <summary>
    /// Pause game
    /// </summary>
    void Pause()
    {
        // Activate pause menu
        pauseMenuUI.SetActive(true);

        //Pause timer
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /// <summary>
    /// Exit game to menu scene
    /// </summary>
    public void ExitGame()
    {
        // Activate timer
        Time.timeScale = 1f;

        // Load menu scene
        SceneManager.LoadScene(0);
    }
}
