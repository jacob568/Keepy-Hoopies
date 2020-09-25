using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject cross;
    public GameObject rewardedAdPanel;

    public TextMeshProUGUI HoopScoredText;
    private Vector3 startScale;
    private Vector3 endScale;

    private Vector3 rewardedAdEndScale;

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
    public Button resume, pauseButton, restartButton;
    //Game over buttons
    public Button retry, returnToMenu, pauseReturnToMain;
    //Main menu buttons

    public Button rewardedAdNoButton;

    private int currentColour;

    public Canvas leftStickCanvas;
    public Canvas rightStickCanvas;
    public Button leftJumpButton;
    public Button rightJumpButton;

    const string HighScoreMessage = "New High Score!";
    const string NewBestMessage = "NEW BEST";

    private TipPanelControl leftTipPanel, rightTipPanel;
    // Start is called before the first frame update
    void Start()
    {
        leftTipPanel = leftStickCanvas.gameObject.GetComponentInChildren<TipPanelControl>();
        rightTipPanel = rightStickCanvas.gameObject.GetComponentInChildren<TipPanelControl>();
        rewardedAdEndScale = Vector3.one;
        startScale = Vector3.zero;
        endScale = new Vector3(3f, 3f, 3f);
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
        pauseReturnToMain.onClick.AddListener(returnToMain);
        rewardedAdNoButton.onClick.AddListener(rewardedAdPanelNoButton);

        soundToggle.isOn = PlayerPersistence.GetSoundStatus() == 1 ? true : false;
        musicToggle.isOn = PlayerPersistence.GetMusicStatus() == 1 ? true : false;
        swapControls.isOn = PlayerPersistence.GetFlipControlStatus() == 1 ? true : false;


    }

    // Update is called once per frame
    void Update()
    {
        if (
            PlayerPrefs.GetInt("swapControls") == 0 
            && (!leftStickCanvas.gameObject.activeSelf || !rightJumpButton.gameObject.activeSelf))
        {
            leftStickCanvas.gameObject.SetActive(true);
            rightJumpButton.gameObject.SetActive(true);
            rightStickCanvas.gameObject.SetActive(true);
            leftJumpButton.gameObject.SetActive(true);
            if (rightTipPanel.GetFaded())
            {
                if (leftStickCanvas.gameObject.activeSelf)
                {
                    leftStickCanvas.gameObject.SetActive(true);
                }
                leftTipPanel.HideText();
            }
            rightStickCanvas.gameObject.SetActive(false);
            leftJumpButton.gameObject.SetActive(false);
        }
        else if (
             PlayerPrefs.GetInt("swapControls") == 1
             && (!rightStickCanvas.gameObject.activeSelf || !leftJumpButton.gameObject.activeSelf))
        {
             rightStickCanvas.gameObject.SetActive(true);
             leftJumpButton.gameObject.SetActive(true);
             leftStickCanvas.gameObject.SetActive(true);
             rightJumpButton.gameObject.SetActive(true);
             if (leftTipPanel.GetFaded())
             {
                 if (!rightStickCanvas.gameObject.activeSelf)
                 {
                     rightStickCanvas.gameObject.SetActive(true);
                 }
                 rightTipPanel.HideText();
             }
             leftStickCanvas.gameObject.SetActive(false);
             rightJumpButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Changes the colour of the cross
    /// </summary>
    /// <param name="colour">The colour to change to, 0 = gray, 1 = red</param>
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

    /// <summary>
    /// Gets the colour of the cross, represented by an int
    /// </summary>
    /// <returns>0 if its gray, 1 if its red</returns>
    public int getCrossColour()
    {
        return currentColour;
    }

    /// <summary>
    /// Loads the title scene
    /// </summary>
    public void returnToMain()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// Toggles the games music
    /// </summary>
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

    /// <summary>
    /// Toggles the game sounds
    /// </summary>
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

    /// <summary>
    /// Toggles the flip of the controls
    /// </summary>
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
    
    /// <summary>
    /// Restarts the game from the game over panel
    /// </summary>
    public void retryGame()
    {
        gameManager.restartGame();
        CloseGameLostPanel();
    }

    /// <summary>
    /// Unpauses the game
    /// </summary>
    public void unpause()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }

    /// <summary>
    /// Toggles the games paused state
    /// </summary>
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

    /// <summary>
    /// Restarts the game
    /// </summary>
    private void restart()
    {
        gameManager.restartGame();
        unpause();
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    private void pause()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        
    }

    /// <summary>
    /// Opens the game open panel
    /// </summary>
    public void OpenGameLostPanel()
    {
        gameLostPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Closes the game over panel
    /// </summary>
    public void CloseGameLostPanel()
    {
        gameLostPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Displays the total score achieved at the end of the game
    /// </summary>
    /// <param name="score">The final score to be displayed</param>
    /// <param name="displayHighScoreText">If it is a high score</param>
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

    /// <summary>
    /// Displays the number of swishes achieved at the end of a game
    /// </summary>
    /// <param name="score">The number of swishes scored</param>
    /// <param name="displayHighScoreText">If its a high score</param>
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

    /// <summary>
    /// Displays the number of hoops achieved at the end of a game
    /// </summary>
    /// <param name="score">The number of hoops scored</param>
    /// <param name="displayHighScoreText">If its a high score</param>
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

    /// <summary>
    /// Updates the score text on the ingame scoreboard
    /// </summary>
    /// <param name="score">The score to display</param>
    public void updateScoreText(float score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    /// <summary>
    /// Updates the highscore text on the ingame highscore scoreboard
    /// </summary>
    /// <param name="highScore">The score to display</param>
    public void updateHighScoreText(float highScore)
    {
        highScoreText.text = "Best: " + highScore.ToString();
    }

    /// <summary>
    /// Displays the text when a hoop or swish is scored
    /// </summary>
    /// <param name="hoopOrSwish">0 tells the code to print HOOP, 1 is SWISH</param>
    /// <returns></returns>
    public IEnumerator HoopScored(int hoopOrSwish)
    {
        if (hoopOrSwish == 0)
        {
            HoopScoredText.text = "HOOP";
        }
        else if (hoopOrSwish == 1)
        {
            HoopScoredText.text = "SWISH";
        }
        float i = 0.0f;
        float rate = (1.0f / 1.2f) * 2f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            HoopScoredText.transform.localScale = Vector3.Lerp(startScale, endScale, i);
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        HoopScoredText.transform.localScale = Vector3.zero;
    }

    /// <summary>
    /// Grows/opens the rewarded ad panel
    /// </summary>
    /// <returns></returns>
    public IEnumerator OpenRewardedAdPanel()
    {
        float i = 0.0f;
        float rate = (1.0f / .4f) * 2f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            rewardedAdPanel.transform.localScale = Vector3.Lerp(startScale, rewardedAdEndScale, i);
            yield return null;
        }
    }

    /// <summary>
    /// Ends the game and closes the reward panel if rewarded ad is declined
    /// </summary>
    public void rewardedAdPanelNoButton()
    {
        gameManager.lostGame();
        StartCoroutine(CloseRewardedAdPanel());
    }

    /// <summary>
    /// Shrinks/closes the rewarded ad offer panel.
    /// </summary>
    /// <returns><IEnumerator/returns>
    public IEnumerator CloseRewardedAdPanel()
    {
        float i = 0.0f;
        float rate = (1.0f / .4f) * 2f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            rewardedAdPanel.transform.localScale = Vector3.Lerp(rewardedAdEndScale, startScale, i);
            yield return null;
        }
    }
}

