using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BS_GameMenu : MonoBehaviour {

    private GameObject[] carrots;
    public TMP_Dropdown difficultyDropdown;
    public BS_GameManager gm;

    /// <summary>
    /// Set difficulty for player
    /// </summary>
    /// <param name="difficultyIndex"></param>
    public void SetDifficulty(int difficultyIndex)
    {
        // If PlayerPrefs is the same don't try set difficulty
        if (PlayerPrefs.GetInt("bs_difficulty", 3) - 1 != difficultyIndex)
        {
            // Set new difficulty and reload scene
            PlayerPrefs.SetInt("bs_difficulty", difficultyIndex + 1);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
