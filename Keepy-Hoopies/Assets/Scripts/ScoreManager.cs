using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private float score, highScore, previousHighScore;
    private UiManager uiManager;

    public GameObject pointsText;
    public Transform sphere;
    private List<GameObject> pointsTexts;

    // Use this for initialization
    void Start () {

        pointsTexts = new List<GameObject>();
        uiManager = GetComponent<UiManager>();
        highScore = PlayerPersistence.GetHighScore();
        uiManager.updateHighScoreText(highScore);
        previousHighScore = 1.5f;
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
    }

    public void addToScore(float toAdd)
    {
        score += toAdd;
        string pointsValue = "+" + toAdd.ToString();
        StartCoroutine(pointsTextMovement(pointsValue));
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
