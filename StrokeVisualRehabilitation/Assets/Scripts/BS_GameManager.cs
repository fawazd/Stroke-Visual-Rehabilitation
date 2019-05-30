using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BS_GameManager : MonoBehaviour {

    bool gameHasEnded = false;
    public GameObject carrot;
    public GameObject exclamation;
    public GameObject backWall;
    public float restartDelay = 1f;
    public TextMeshProUGUI scoreText;
    float respawnRate = 10f;
    int score = 0;
    int count = 0;
    int obstacleSpeed = 15;
    List<GameObject> carrots = new List<GameObject>();
    List<GameObject> exclamations = new List<GameObject>();
    public AudioSource chompSource;
    public AudioClip chompSound;
    public TMP_Dropdown difficultyDropdown;

    public void Start()
    {
        respawnRate = 10 - PlayerPrefs.GetInt("difficulty", 3);
        difficultyDropdown.value = PlayerPrefs.GetInt("difficulty", 3) - 1;
        InvokeRepeating("CreateCarrot", 0f, respawnRate);
    }

    public void Update()
    {
        int index = 0;
        foreach (GameObject carrot in carrots.ToList())
        {
            if (carrot == null)
            {
                carrots.RemoveAt(index);
                Destroy(exclamations[index]);
                exclamations.RemoveAt(index);
            }
            index++;
        }
    }

    public void PlayChomp()
    {
        chompSource.PlayOneShot(chompSound);
    }

    public void CreateCarrot()
    {
        float x = Random.Range(-6.5f, 6.5f);
        GameObject newCarrot = Instantiate(carrot, new Vector3(x, 1f, 130f), transform.rotation * Quaternion.Euler(45.94f, 90f, -90f));
        newCarrot.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -obstacleSpeed);
        carrots.Add(newCarrot);
        GameObject newExclamation = Instantiate(exclamation, new Vector3(x, 0.34f, -39.94f), transform.rotation * Quaternion.Euler(42.506f, 0f, 0f));
        exclamations.Add(newExclamation);
    }


    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
