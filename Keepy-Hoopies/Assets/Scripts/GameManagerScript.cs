using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
    //public GameObject skinScreen;
    private SceneController sceneController;
    private UiManager uiManager;
    public PlayerControl playerControl;
    private ScoreManager scoreManager;
    private bool gameStarted;
    private bool gameLost;
    private bool gameReady;
    public bool paused;
    public SpherePhysics spherePhysics;
    private CountdownTimer countdownTimer;

    private bool musicOn, soundsOn;

    private float maxNumberOfDrops;

    // Use this for initialization
    void Start() {
        gameLost = false;
        uiManager = GetComponent<UiManager>();
        scoreManager = GetComponent<ScoreManager>();
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
        countdownTimer = GetComponent<CountdownTimer>();
        gameReady = false;
        sceneController = new SceneController();

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
        }

        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }


    }

    // Update is called once per frame
    void Update() {
        //Change this so not calling every frame
        if (spherePhysics.getBallDrops() == 0)
        {
            uiManager.ChangeCrossColour(1);
        } else
        {
            uiManager.ChangeCrossColour(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }

        if (!gameReady)
        {
            gameStarted = false;
        }

        if (gameLost)
        {
            float score = scoreManager.getScore();
            float currentHighScore = PlayerPersistence.GetHighScore();
            if (score > currentHighScore)
            {
                PlayerPersistence.SetHighScore(score);
                scoreManager.updateHighScore();
                uiManager.newHighScore(score);
            }
            uiManager.displayFinalScore(score);
        }

        //if (paused)
        //{
        //    Time.timeScale = 0;
        //    pauseMenu.gameObject.SetActive(true);
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //    pauseMenu.gameObject.SetActive(false);
        //}
    }

    public void quitGame()
    {
        gameStarted = false;
        unpauseGame();
        returnToMain();
        restartGame();
    }

    public void returnToMain()
    {
        //StartCoroutine(waitForAnimationMenu());
    }

    public void pauseGame()
    {
        paused = true;
    }

    public void restartGame()
    {   
        //spherePhysics.returnToCannon();
        scoreManager.resetScore();
        gameLost = false;
        countdownTimer.refresh();
        playerControl.resetLocation();
        gameReady = true;
    }


    public void quitToMain()
    {
        Debug.Log("Quit game");
    }

    public void unpauseGame()
    {
        paused = false;
    }

    public bool getPausedState()
    {
        return paused;
    }

    public float getMaxDrops()
    {
        return maxNumberOfDrops;
    }

    public bool getGameLostState()
    {
        return gameLost;
    }

    public void lostGame()
    {
        gameReady = false;
        gameStarted = false;
        gameLost = true;
    }

    public void startGame()
    {
        gameStarted = true;
    }

    public bool getGameState()
    {
        return gameStarted;
    }

    private void setGameReady(bool state)
    {
        gameReady = state;
    }

    public bool getGameReadyState()
    {
        return gameReady;
    }

    private void togglePause()
    {
        if (!gameLost)
        {
            uiManager.togglePause();
            paused = !paused;
        }
    }

    public void loadGame()
    {
        restartGame();
        gameLost = false;
        //StartCoroutine(waitForAnimationGame());
    }

    //private IEnumerator waitForAnimationGame()
    //{
    //    yield return new WaitForSeconds(2f);
    //    controlCanvas.SetActive(true);
    //    setGameReady(true);
    //}

    //private IEnumerator waitForAnimationMenu()
    //{
    //    controlCanvas.SetActive(false);
    //    yield return new WaitForSeconds(2f);
    //    setGameReady(false);
    //}
}
