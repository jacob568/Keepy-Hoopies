using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private float score, highScore, previousHighScore;
    private UiManager uiManager;

	// Use this for initialization
	void Start () {
        uiManager = GetComponent<UiManager>();
        highScore = PlayerPersistence.GetHighScore();
        uiManager.updateHighScoreText(highScore);
        previousHighScore = 1.5f;
        score = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        uiManager.updateScoreText(score);
	}

    public void addToScore(float toAdd)
    {
        score += toAdd;
    }

    public void resetScore()
    {
        score = 0;
    }

    public float getScore()
    {
        return score;
    }

    public void gameOverScoreManagement()
    {
        bool isHighScore = false;
        Debug.Log(score + " " + highScore);
        if (score > highScore)
        {
            isHighScore = true;
            PlayerPersistence.SetHighScore(score);
        }
        uiManager.OpenGameLostPanel();
        uiManager.displayFinalScore(score, isHighScore);
    }

}
