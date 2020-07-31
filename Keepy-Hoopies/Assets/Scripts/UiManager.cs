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

    public Toggle musicToggle, soundToggle, swapControls;

    public TextMeshProUGUI highScoreAchieved;
    public TextMeshProUGUI gameOverScoreText;

    public TextMeshProUGUI swishesText, hoopsText, swishNewBest, hoopNewBest;
    //public TextMeshProUGUI scoreText;
    public TextMeshPro scoreText;
    //public TextMeshProUGUI highScoreText;
    public TextMeshPro highScoreText;

    //Pause buttons
    public Button resume, quit, pauseButton, restartButton;
    //Game over buttons
    public Button retry, returnToMenu;
    //Main menu buttons
    public Button startGameButton;

    private int currentColour;

    public Canvas leftStickCanvas;
    public Canvas rightStickCanvas;
    public Button leftJumpButton;
    public Button rightJumpButton;

    const string HighScoreMessage = "New High Score!";
    const string NewBestMessage = "NEW BEST";
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
        swapControls.onValueChanged.AddListener(delegate
        {
            toggleFlipControl();
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
        swapControls.isOn = PlayerPersistence.GetFlipControlStatus() == 1 ? true : false;


    }

    // Update is called once per frame
    void Update()
    {
        if (
                PlayerPrefs.GetInt("swapControls") == 0 
                && (!leftStickCanvas.gameObject.activeSelf || !rightJumpButton.gameObject.activeSelf)
            )
        {
            leftStickCanvas.gameObject.SetActive(true);
            rightJumpButton.gameObject.SetActive(true);
            rightStickCanvas.gameObject.SetActive(false);
            leftJumpButton.gameObject.SetActive(false);
        }
        else if (
                PlayerPrefs.GetInt("swapControls") == 1
                && (!rightStickCanvas.gameObject.activeSelf || !leftJumpButton.gameObject.activeSelf)
            )
        {
            leftStickCanvas.gameObject.SetActive(false);
            rightJumpButton.gameObject.SetActive(false);
            rightStickCanvas.gameObject.SetActive(true);
            leftJumpButton.gameObject.SetActive(true);
        }
    }

    //0 = gray
    //1 = red
    public void ChangeCrossColour(int colour)
    {
        SpriteRenderer crossImage = cross.GetComponent<SpriteRenderer>();
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

    public void toggleFlipControl()
    {
        if (swapControls.isOn)
        {
            PlayerPersistence.FlipControlsToggle(true);
        }
        else
        {
            PlayerPersistence.FlipControlsToggle(false);
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
        if (!gameManager.getGameLostState())
        {
            if (gameObject.activeSelf)
            {
                pause();
            } else
            {
                unpause();
            }
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

    public void displaySwishFinalScore(float score, bool displayHighScoreText)
    {
        swishesText.text = "Swishes: " + score.ToString();
        if (displayHighScoreText)
        {
            swishNewBest.text = NewBestMessage;
        }
        else
        {
            swishNewBest.text = "";
        }
    }

    public void displayHoopFinalScore(float score, bool displayHighScoreText)
    {
        hoopsText.text = "Hoops: " + score;
        if (displayHighScoreText)
        {
            hoopNewBest.text = NewBestMessage;
        }
        else
        {
            hoopNewBest.text = "";
        }
    }

    public void updateScoreText(float score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void updateHighScoreText(float highScore)
    {
        highScoreText.text = "Best: " + highScore.ToString();
    }
}
