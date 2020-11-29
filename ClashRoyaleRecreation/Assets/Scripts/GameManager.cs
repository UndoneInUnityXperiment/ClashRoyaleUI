using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//Created following this guide: https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
public class GameManager : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public Color32 timerRed;
    public Image elixirBar;
    public float currentElixir;
    public float elixirMax = 10f;
    public float elixirIncrease;
    public TextMeshProUGUI elixirText;
    public TextMeshProUGUI maxText;
    public TextMeshProUGUI redScoreShown;
    public TextMeshProUGUI blueScoreShown;
    int redScore, blueScore;
    public GameObject[] cards;
    int cardToSpawn;
    public GameObject newPosition;
    public GameObject newCard;
    public GameObject oldCard;
    public GameObject canvas;
    public Vector3 emptyPosition;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        currentElixir = 0f;
        maxText.text = elixirMax.ToString();
        redScore = 0;
        blueScore = 0;
        SpawnCard();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
            if (timeRemaining < 170)
            {
                timeText.faceColor = timerRed;
            }
        }

        if (currentElixir < elixirMax)
        {
            currentElixir += elixirIncrease;
            elixirBar.fillAmount = currentElixir / elixirMax;
        }

        elixirText.text = Mathf.FloorToInt(currentElixir).ToString();
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown("up"))
        {
            redScore += 1;
            redScoreShown.text = redScore.ToString();
        }

        if (Input.GetKeyDown("down"))
        {
            blueScore += 1;
            blueScoreShown.text = blueScore.ToString();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void SpawnCard()
    {
        Debug.Log("Spawned a card");
        oldCard = newCard;
        cardToSpawn = Mathf.RoundToInt(Random.Range(0, 3));
        newCard = Instantiate(cards[cardToSpawn], newPosition.transform.position, Quaternion.identity);
        newCard.transform.SetParent(canvas.transform, false);
        newCard.transform.position = newPosition.transform.position;
        newCard.GetComponentInChildren<CardScript>().SetSmall();
    }
}
