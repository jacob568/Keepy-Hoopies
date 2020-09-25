using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private float score, previousScore, highScore, swishBest, hoopBest, hoopsScored, swishesScored;
    private GoogleLeaderboards leaderboards;
    private GoogleAchievements achievements;
    private UiManager uiManager;

    public GameObject pointsTextObject;
    private GameObject newPointsText;
    public Transform sphere;
    private List<GameObject> pointsTexts;

    // Use this for initialization
    void Start ()
    {
        achievements = GetComponent<GoogleAchievements>();
        leaderboards = GetComponent<GoogleLeaderboards>();
        pointsTexts = new List<GameObject>();
        uiManager = GetComponent<UiManager>();
        highScore = PlayerPersistence.GetHighScore();
        hoopBest = PlayerPersistence.GetHoopHighScore();
        swishBest = PlayerPersistence.GetSwishHighScore();
        swishesScored = 0;
        hoopsScored = 0;
        uiManager.updateHighScoreText(highScore);
        previousScore = -1f;
        score = 0f;
        newPointsText = Object.Instantiate(pointsTextObject, Vector3.zero, Quaternion.identity);
        newPointsText.transform.localScale = Vector3.zero;

    }

    // Update is called once per frame
    void Update () {
        if (score != previousScore)
        {
            uiManager.updateScoreText(score);
            previousScore = score;
        }

        if (pointsTexts.Count != 0)
        {
            foreach (GameObject text in pointsTexts)
            {
                text.transform.position = text.transform.position + new Vector3(0f, 1f, 0f) * 5f * Time.deltaTime;
            }
        }
        if (hoopsScored == 2)
        {
            achievements.UnlockDoubleHoop();
        }

        if (swishesScored == 2)
        {
            achievements.UnlockDoubleSwish();
        }
    }

    /// <summary>
    /// Increases the score by a given value and initiates the floating score text
    /// </summary>
    /// <param name="toAdd"></param>
    public void addToScore(float toAdd)
    {
        score += toAdd;
        StartCoroutine(pointsTextMovement("+" + toAdd.ToString()));
    }

    /// <summary>
    /// This manages when a hoop is scored by the player
    /// </summary>
    public void scoreHoop()
    {
        achievements.UnlockHoop();
        StartCoroutine(uiManager.HoopScored(0));
        addToScore(5f);
        hoopsScored++;
    }

    /// <summary>
    /// This manages when a swish is scored by the player
    /// </summary>
    public void scoreSwish()
    {
        achievements.UnlockSwish();
        StartCoroutine(uiManager.HoopScored(1));
        addToScore(10f);
        swishesScored++;
    }

    /// <summary>
    /// Resets the score for a new game.
    /// </summary>
    public void resetScore()
    {
        score = 0;
        swishesScored = 0;
        hoopsScored = 0;
    }


    /// <returns>The currentt score</returns>
    public float getScore()
    {
        return score;
    }

    /// <summary>
    /// This manages the scores when the game has ended. Updating leaderboards and checking achievements
    /// and displaying the correct text on the game over screen.
    /// </summary>
    public void gameOverScoreManagement()
    {
        leaderboards.updateHighScoreLeaderboard((long)score);
        leaderboards.updateSwishHighScoreLeaderboard((long)swishesScored);
        leaderboards.updateHoopHighScoreLeaderboard((long)hoopsScored);
        bool isHighScore = false;
        bool isSwishBest = false;
        bool isHoopBest = false;

        achievements.CheckScoreAchievements(score);
        if (score > highScore)
        {
            isHighScore = true;
            uiManager.updateHighScoreText(score);
            PlayerPersistence.SetHighScore(score);
        }
        if (swishesScored > swishBest)
        {
            isSwishBest = true;
            PlayerPersistence.SetSwishHighScore(swishesScored);
        }
        if (hoopsScored > hoopBest)
        {
            isHoopBest = true;
            PlayerPersistence.SetHoopHighScore(hoopsScored);
        }

        uiManager.OpenGameLostPanel();
        uiManager.displayFinalScore(score, isHighScore);
        uiManager.displaySwishFinalScore(swishesScored, isSwishBest);
        uiManager.displayHoopFinalScore(hoopsScored, isHoopBest);
    }

    /// <summary>
    /// Positions and displays the +{score} text when points are scored
    /// </summary>
    /// <param name="value">The number of points scored</param>
    IEnumerator pointsTextMovement(string value)
    {
        float xOffset = 10f;
        Vector3 newPos = new Vector3(sphere.position.x + xOffset, sphere.position.y, sphere.position.z);
        newPointsText.transform.position = newPos;
        newPointsText.transform.localScale = Vector3.one;
        pointsTexts.Add(newPointsText);
        newPointsText.GetComponent<TextMeshPro>().text = value;
        yield return new WaitForSeconds(.7f);
        pointsTexts.Remove(newPointsText);
        newPointsText.transform.localScale = Vector3.zero;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The current high score</returns>
    public float GetHighScore()
    {
        return highScore;
    }

}
