using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Tobii.Gaming;

public class BRGameManager : MonoBehaviour {

    public GameObject BRBrick;
    public BRBall ball;
    private int score = 0;
    public Text scoreText;
    private int lives = 3;
    public Text livesText;
    private int level = 1;
    public Text levelText;
    public GameObject pausedPanel;
    public GameObject gameOverPanel;
    private bool gameIsPaused;
    public BRPaddle paddle;

    // Use this for initialization
    void Start () {
        if (double.IsNaN(TobiiAPI.GetGazePoint().Screen.x))
        {
            RestartGame();
        }
        Time.timeScale = 1f;
        GenerateBricks();
	}
	
	// Update is called once per frame
	void Update () {
        CheckGameOver();
        CheckLevelFinish();

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        scoreText.text = "Score: " + score.ToString();
        levelText.text = "Level: " + level.ToString();
        livesText.text = "Lives: " + lives.ToString();
    }

    public void PauseResumeGame()
    {
        gameIsPaused = !gameIsPaused;
        paddle.PaddlePaused = !paddle.PaddlePaused;
        pausedPanel.SetActive(gameIsPaused);
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void GenerateBricks()
    {
        float x = -30;
        float z = 10;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject obj = Instantiate(BRBrick, new Vector3(x + (BRBrick.transform.localScale.x * j) + j, 2f, z + (BRBrick.transform.localScale.z * i) + i), Quaternion.identity);
                obj.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }

    private void CheckGameOver()
    {
        if(lives < 0)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }
    }

    private void CheckLevelFinish()
    {
        if(GameObject.Find("BRBrick(Clone)") == null)
        {
            ball.ResetBall();
            GenerateBricks();
            level++;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("BreakOutScene");
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public int Score{ get{ return score;} set{ score = value; } }

    public int Lives{ get {return lives;}set{lives = value;} }

    public bool GameIsPaused
    { get { return gameIsPaused; } set { gameIsPaused = value; } }
}
