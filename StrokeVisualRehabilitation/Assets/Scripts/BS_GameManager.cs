using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BS_GameManager : MonoBehaviour {

    // Public variables
    public GameObject carrot;
    public GameObject exclamation;
    public GameObject backWall;
    public float restartDelay = 1f;
    public TextMeshProUGUI scoreText;
    public AudioSource chompSource;
    public AudioClip chompSound;
    public TMP_Dropdown difficultyDropdown;

    // Private variables
    float respawnRate = 10f;
    int score = 0;
    int count = 0;
    int obstacleSpeed = 15;
    List<GameObject> carrots = new List<GameObject>();
    List<GameObject> exclamations = new List<GameObject>();
    bool gameHasEnded = false;

    /// <summary>
    /// Use PlayerPrefs to setup difficulty and set CreateCarrot method to run at respawn rate
    /// </summary>
    public void Start()
    {
        respawnRate = 10 - PlayerPrefs.GetInt("bs_difficulty", 3);
        difficultyDropdown.value = PlayerPrefs.GetInt("bs_difficulty", 3) - 1;
        InvokeRepeating("CreateCarrot", 0f, respawnRate);
    }

    /// <summary>
    /// On game update check if carrot has been destroyed then remove from list and destroy exclamation associated with that carrot
    /// </summary>
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
                ExclamationChange();
            }
            index++;
        }

    }

    /// <summary>
    /// Play sound when carrot is eaten
    /// </summary>
    public void PlayChomp()
    {
        chompSource.PlayOneShot(chompSound);
    }

    /// <summary>
    /// Create a carrot prefab
    /// </summary>
    public void CreateCarrot()
    {
        // Randomly choose where it will spawn
        float x = Random.Range(-6.5f, 6.5f);

        // Create carrot and add velocity
        GameObject newCarrot = Instantiate(carrot, new Vector3(x, 1f, 130f), transform.rotation * Quaternion.Euler(45.94f, 90f, -90f));
        newCarrot.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -obstacleSpeed);
        carrots.Add(newCarrot);

        // Create exclamation hint for that carrot
        GameObject newExclamation = Instantiate(exclamation, new Vector3(x, 0.34f, -39.94f), transform.rotation * Quaternion.Euler(42.506f, 0f, 0f));
        exclamations.Add(newExclamation);
        ExclamationChange();
    }

    /// <summary>
    /// End game when it has finished
    /// Still needs implemented
    /// </summary>
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
        }
    }

    /// <summary>
    /// Increase player score
    /// </summary>
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Restart game
    /// Still needs implemented
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Change color of exclamation hint of closest hint to be highlighted
    /// </summary>
    public void ExclamationChange()
    {
        GameObject cylinder = exclamations[0].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        GameObject sphere = exclamations[0].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        cylinder.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/BS_FirstExclam");
        sphere.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/BS_FirstExclam");
    }
}
