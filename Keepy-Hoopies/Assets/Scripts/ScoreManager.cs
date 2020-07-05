using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private float score, highScore;
    private UiManager uiManager;

	// Use this for initialization
	void Start () {
        uiManager = GetComponent<UiManager>();
        highScore = PlayerPersistence.GetHighScore();
        score = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        uiManager.updateScoreText(score, highScore);
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

    public void updateHighScore()
    {
        highScore = PlayerPersistence.GetHighScore();
    }
}
