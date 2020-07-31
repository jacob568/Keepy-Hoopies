using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button playButton, leaderboardButton, achievementsButton, helpButton, closeHelpMenu;
    public TextMeshProUGUI scoreText;
    public GameObject helpPanel;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
        helpButton.onClick.AddListener(OpenHelpPanel);
        closeHelpMenu.onClick.AddListener(CloseHelpPanel);
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

    private void OpenHelpPanel()
    {
        helpPanel.SetActive(true);
    }

    private void CloseHelpPanel()
    {
        helpPanel.SetActive(false);
    }


}
