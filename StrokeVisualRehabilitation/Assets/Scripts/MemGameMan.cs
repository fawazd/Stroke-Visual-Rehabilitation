using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using System;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MemGameMan : MonoBehaviour
{

    public CardScript card;
    public CardScript bigCard;
    private List<CardScript> allCards;
    private List<CardScript> cardsSelected;

    private int pairs = 0;

    private float animTime = 2f;
    private bool animTimerOn;

    private bool difficult;

    public TMP_Text timeText;
    public TMP_Text pairFoundText;
    public TMP_Text pairsText;
    public TMP_Text timeTakenText;
    //public TMP_Text cancelSelectionText;

    public TMP_Dropdown difficultyDropdown;

    private bool gameIsPaused;
    public GameObject pausedPanel;
    public GameObject gameOverPanel;

    //public AudioClip notAPair;
    public AudioClip pairfound;

    public List<CardScript> Cards { get { return cardsSelected; } set { cardsSelected = value; } }

    public bool GameIsPaused
    { get { return gameIsPaused; } set { gameIsPaused = value; } }

    // Use this for initialization
    void Start()
    {
        difficultyDropdown.value = PlayerPrefs.GetInt("mem_difficulty", 0);
        if (PlayerPrefs.GetInt("mem_difficulty", 0) == 0)
        {
            difficult = false;
        }
        else
        {
            difficult = true;
        }
        Time.timeScale = 1f;
        allCards = new List<CardScript>();
        cardsSelected = new List<CardScript>();
        GenerateCards();
    }

    // Update is called once per frame
    void Update()
    {
        //if(RectTransformUtility.RectangleContainsScreenPoint(cancelSelectionText.rectTransform, TobiiAPI.GetGazePoint().Screen) && cards.Count == 1)
        //{
        //    cancelSelectionText.color = Color.blue;
        //    //Reset Cards Selected
        //    foreach (CardScript card in cards)
        //    {
        //        card.Animate("FlipDown");
        //    }
        //    cards.Clear();
        //}
        UpdateGameTime();
        pairsText.text = "Pairs found: " + pairs.ToString();

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        if (cardsSelected.Count == 2)
        {
            animTimerOn = true;
            if (animTime <= 0f)
            {
                if (CheckCards())
                {
                    StartCoroutine(ShowMessage("Pair Found", 2));
                }
                else
                {
                    StartCoroutine(ShowMessage("Try Again", 2));
                }
            }
        }

        if (pairs == 9 && difficult || pairs == 4 && !difficult)
        {
            EndGame();
        }

        if (animTimerOn)
        {
            animTime -= Time.deltaTime;
        }
    }
    IEnumerator ShowMessage(string message, float delay)
    {
        pairFoundText.text = message;
        pairFoundText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        pairFoundText.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        timeTakenText.text = timeText.text;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MemoryGameScene");
    }
    public void PauseResumeGame()
    {
        gameIsPaused = !gameIsPaused;
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

    private void UpdateGameTime()
    {
        string fmt = "00.00";
        float time = Time.timeSinceLevelLoad;

        string minutes = ((int)time / 60).ToString("D2");
        string seconds = (time % 60).ToString(fmt);
        timeText.text = String.Format("Time: {0}:{1}", minutes, seconds);
    }

    private bool CheckCards()
    {
        bool pair = false;
        if (cardsSelected[0].transform.GetChild(2).GetComponent<Renderer>().sharedMaterial == cardsSelected[1].transform.GetChild(2).GetComponent<Renderer>().sharedMaterial)
        {
            foreach (CardScript card in cardsSelected)
            {
                card.Animate("PairFound", pairfound);
            }
            pairs++;
            cardsSelected.Clear();
            pair = true;
        }
        else
        {
            foreach (CardScript card in cardsSelected)
            {
                card.Animate("FlipDown", null);
            }
            cardsSelected.Clear();
            pair = false;
        }

        animTimerOn = false;
        animTime = 2f;
        return pair;
    }

    public void ChangeDifficulty(int index)
    {
        if (PlayerPrefs.GetInt("mem_difficulty", 0) != index)
        {
            PlayerPrefs.SetInt("mem_difficulty", index);
            Time.timeScale = 1f;
            RestartGame();
        }
    }

    private void GenerateCards()
    {
        float x, z;
        List<int> colors = new List<int>();
        int colorAmount, across, down, count = 0;

        if (difficult)
        {
            x = -7.5f;
            z = -7.5f;
            colorAmount = 9;
            across = 6;
            down = 3;
        }
        else
        {
            x = -5.5f;
            z = -7f;
            colorAmount = 4;
            across = 4;
            down = 2;
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < colorAmount; j++)
            {
                colors.Add(j);
            }
        }

        colors = ShuffleList(colors);

        for (int i = 0; i < down; i++)
        {
            for (int j = 0; j < across; j++)
            {
                GameObject emptyObj = new GameObject();
                CardScript newCard;
                if (difficult)
                {
                    emptyObj.transform.localPosition = new Vector3(x + (card.transform.localScale.x * j) + j, 0f, z + (card.transform.localScale.z * i) + i);
                    newCard = Instantiate(card, emptyObj.transform) as CardScript;
                }
                else
                {
                    emptyObj.transform.localPosition = new Vector3(x + (bigCard.transform.localScale.x * j) + j, 0f, z + (bigCard.transform.localScale.z * i) + i);
                    newCard = Instantiate(bigCard, emptyObj.transform) as CardScript;
                }
                newCard.transform.GetChild(2).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/MemCardColor" + colors[count]);
                allCards.Add(newCard);
                count++;
            }
        }
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private List<int> ShuffleList(List<int> inputList)
    {
        List<int> randomList = new List<int>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
            randomList.Add(inputList[randomIndex]); //add it to the new, random list
            inputList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }

    //Add settings.
    //Add card designs.

    //Done
}