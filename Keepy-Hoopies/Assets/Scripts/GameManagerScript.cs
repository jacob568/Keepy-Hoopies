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
        uiManager = GetComponent<UiManager>();
        scoreManager = GetComponent<ScoreManager>();
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
        countdownTimer = GetComponent<CountdownTimer>();
        sceneController = new SceneController();
        spherePhysics.toggleGravity();

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
        }

        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }

        gameLost = false;
        gameReady = true;
        gameStarted = false;

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPersistence.SetHighScore(0);
        }
        //Checks which colour the cross needs to be
        if (spherePhysics.getBallDrops() == 0 && uiManager.getCrossColour() == 0)
        {
            uiManager.ChangeCrossColour(1);
        }
        else if (spherePhysics.getBallDrops() > 0 && uiManager.getCrossColour() == 1)
        {
            uiManager.ChangeCrossColour(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }


        if (gameLost)
        {
            float score = scoreManager.getScore();
            float currentHighScore = PlayerPersistence.GetHighScore();
            //This needs to go before for the new high score to appear correctly
            scoreManager.gameOverScoreManagement();
        }
    }

    public void quitGame()
    {
        gameStarted = false;

        restartGame();
    }


    /// <summary>
    /// Resets everything in the game for another run
    /// </summary>
    public void restartGame()
    {   
        spherePhysics.returnToStartPosition();
        spherePhysics.disableGravity();
        scoreManager.resetScore();
        gameLost = false;
        countdownTimer.refresh();
        playerControl.resetLocation();
        uiManager.CloseGameLostPanel();
        gameStarted = false;
        gameReady = true;
    }

    /// <returns>True if the game is paused</returns>
    public bool getPausedState()
    {
        return paused;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>True if the game has been lost</returns>
    public bool getGameLostState()
    {
        return gameLost;
    }
    /// <summary>
    /// Sets the variables for when the game has been lost
    /// </summary>
    public void lostGame()
    {
        gameReady = false;
        gameStarted = false;
        gameLost = true;
    }

    /// <summary>
    /// Begins the game
    /// </summary>
    public void startGame()
    {
        gameStarted = true;
        spherePhysics.enableGravity();
    }

    public bool getGameState()
    {
        return gameStarted;
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
}
