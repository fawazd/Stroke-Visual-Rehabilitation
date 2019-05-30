using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BS_GameMenu : MonoBehaviour {

    private GameObject[] carrots;
    public TMP_Dropdown difficultyDropdown;
    public BS_GameManager gm;

    public void SetDifficulty(int difficultyIndex)
    {
        if (PlayerPrefs.GetInt("difficulty", 3) - 1 != difficultyIndex)
        {
            PlayerPrefs.SetInt("difficulty", difficultyIndex + 1);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
