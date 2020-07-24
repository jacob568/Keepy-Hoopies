using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private float score, highScore, swishBest, hoopBest, hoopsScored, swishesScored;
    private GoogleLeaderboards leaderboards;
    private GoogleAchievements achievements;
    private UiManager uiManager;

    public GameObject pointsText;
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
        score = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        uiManager.updateScoreText(score);

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

    public void addToScore(float toAdd)
    {
        score += toAdd;
        string pointsValue = "+" + toAdd.ToString();
        StartCoroutine(pointsTextMovement(pointsValue));
    }

    public void scoreHoop()
    {
        achievements.UnlockHoop();
        addToScore(5f);
        hoopsScored++;
    }

    public void scoreSwish()
    {
        achievements.UnlockSwish();
        addToScore(10f);
        swishesScored++;
    }

    public void resetScore()
    {
        score = 0;
        swishesScored = 0;
        hoopsScored = 0;
    }

    public float getScore()
    {
        return score;
    }

    public void gameOverScoreManagement()
    {
        leaderboards.updateHighScoreLeaderboard((long)score);
        leaderboards.updateSwishHighScoreLeaderboard((long)swishesScored);
        leaderboards.updateHoopHighScoreLeaderboard((long)hoopsScored);
        bool isHighScore = false;
        bool isSwishBest = false;
        bool isHoopBest = false;
        Debug.Log(score + " " + highScore);
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

    public void SpawnPointsText(string value)
    {
        StartCoroutine(pointsTextMovement(value));
    }

    IEnumerator pointsTextMovement(string value)
    {
        float xOffset = 10f;
        Vector3 newPos = new Vector3(sphere.position.x + xOffset, sphere.position.y, sphere.position.z);
        GameObject newPointsText = Object.Instantiate(pointsText, newPos, Quaternion.identity);
        pointsTexts.Add(newPointsText);
        newPointsText.GetComponent<TextMeshPro>().text = value;
        yield return new WaitForSeconds(.7f);
        pointsTexts.Remove(newPointsText);
        Destroy(newPointsText);
    }

}
