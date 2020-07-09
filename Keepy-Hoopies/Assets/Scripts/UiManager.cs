using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject cross;

    public RectTransform pauseMenu;
    public RectTransform gameLostPanel;

    private GameManagerScript gameManager;

    public Toggle musicToggle, soundToggle;

    public TextMeshProUGUI highScoreAchieved;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    //Pause buttons
    public Button resume, quit, pauseButton, restartButton;
    //Game over buttons
    public Button retry, returnToMenu;
    //Main menu buttons
    public Button startGameButton;

    private int currentColour;

    const string HighScoreMessage = "New High Score!";
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManagerScript>();
        musicToggle.onValueChanged.AddListener(delegate {
            toggleMusic();
        });
        soundToggle.onValueChanged.AddListener(delegate
        {
            toggleSound();
        });
        resume.onClick.AddListener(unpause);
        retry.onClick.AddListener(retryGame);
        pauseButton.onClick.AddListener(togglePause);
        restartButton.onClick.AddListener(restart);
        returnToMenu.onClick.AddListener(returnToMain);
        //quit.onClick.AddListener(quitGame);
        //startGameButton.onClick.AddListener(loadGame);

        soundToggle.isOn = PlayerPersistence.GetSoundStatus() == 1 ? true : false;
        musicToggle.isOn = PlayerPersistence.GetMusicStatus() == 1 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //0 = gray
    //1 = red
    public void ChangeCrossColour(int colour)
    {
        Image crossImage = cross.GetComponent<Image>();
        if (colour == 1)
        {
            currentColour = 1;
            crossImage.color = Color.red;
        }
        else if (colour == 0)
        {
            currentColour = 0;
            crossImage.color = Color.gray;
        }
    }

    public int getCrossColour()
    {
        return currentColour;
    }

    public void returnToMain()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void toggleMusic()
    {
        if (musicToggle.isOn)
        {
            PlayerPersistence.SetMusicToggle(true);
        }
        else
        {
            PlayerPersistence.SetMusicToggle(false);
        }
    }

    public void toggleSound()
    {
        if (soundToggle.isOn)
        {
            PlayerPersistence.SetSoundToggle(true);
        }
        else
        {
            PlayerPersistence.SetSoundToggle(false);
        }
    }

    public void retryGame()
    {
        gameManager.restartGame();
        CloseGameLostPanel();
    }

    public void unpause()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }

    public void togglePause()
    {
        if (gameObject.activeSelf)
        {
            pause();
        } else
        {
            unpause();
        }
    }

    private void restart()
    {
        gameManager.restartGame();
        unpause();
    }

    private void pause()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        
    }

    public void OpenGameLostPanel()
    {
        gameLostPanel.gameObject.SetActive(true);
    }

    public void CloseGameLostPanel()
    {
        gameLostPanel.gameObject.SetActive(false);
    }

    public void displayFinalScore(float score, bool displayHighScoreText)
    {
        gameOverScoreText.text = "Final Score: " + score;
        if (displayHighScoreText)
        {
            highScoreAchieved.text = HighScoreMessage;
        } else
        {
            highScoreAchieved.text = "";
        }
        //scoreText.text = "Score: " + score;
    }

    public void updateScoreText(float score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void updateHighScoreText(float highScore)
    {
        highScoreText.text = "High score: " + highScore.ToString();
    }
}
