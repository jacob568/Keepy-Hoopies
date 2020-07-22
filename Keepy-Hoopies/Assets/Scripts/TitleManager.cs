using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button playButton, leaderboardButton, achievementsButton;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        achievementsButton.onClick.AddListener(OpenAchievementsPanel);
        leaderboardButton.onClick.AddListener(OpenLeaderboardPanel);
        scoreText.text = PlayerPersistence.GetHighScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OpenAchievementsPanel()
    {
        Social.ShowAchievementsUI();
    }

    private void OpenLeaderboardPanel()
    {
        Social.ShowLeaderboardUI();
    }
}
