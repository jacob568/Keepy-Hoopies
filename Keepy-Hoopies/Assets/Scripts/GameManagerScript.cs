using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
    public AdDisplay ads;
    public RewardedAdDisplay rewardedAds;
    private UiManager uiManager;
    public PlayerControl playerControl;
    private ScoreManager scoreManager;
    private bool gameStarted;
    public bool gameLost;
    private bool gameReady;
    public bool paused;
    public BallController spherePhysics;
    private CountdownTimer countdownTimer;

    private bool extraRound;

    private bool musicOn, soundsOn;

    private float maxNumberOfDrops;

    private bool adFailedToLoad;
    private int gamesPlayed = 0;
    private int adDelay = 0;
    private int gamesForRewardedAd = 4;
    private bool rewardedAdDelay;

    // Use this for initialization
    void Start() {
        extraRound = false;
        adFailedToLoad = false;
        uiManager = GetComponent<UiManager>();
        scoreManager = GetComponent<ScoreManager>();
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        Screen.autorotateToPortrait = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.SetResolution(Screen.width, Screen.height, true);
        countdownTimer = GetComponent<CountdownTimer>();
        spherePhysics.toggleGravity();

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
        }

        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }

        if (!PlayerPrefs.HasKey("swapControls"))
        {
            PlayerPrefs.SetInt("swapControls", 1);
        }

        gameLost = false;
        gameReady = true;
        gameStarted = false;
    }

    // Update is called once per frame
    void Update() {
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

    public bool GetOfferAdState()
    {
        if (!extraRound && rewardedAds.IsAdReady())
        {
            return true;
        }

        return false;

        //if (adDelay > 0)
        //{
        //    adDelay--;
        //}

        //float highScore = scoreManager.GetHighScore();
        //float scoreForRewardedAd = Mathf.Floor(highScore * 0.6f);
        //float score = scoreManager.getScore();

        //if (score >= scoreForRewardedAd && adDelay == 0)
        //{
        //    adDelay = 2;
        //    return true;
        //}
        //else if (gamesPlayed >= gamesForRewardedAd)
        //{
        //    adDelay = 2;
        //    return true;
        //}

        //return false;
    }

    public void SetExtraRound()
    {
        extraRound = true;
    }

    /// <returns>True if the game is paused</returns>
    public bool getPausedState()
    {
        return paused;
    }

    /// <summary>
    /// Returns whether the game is lost or not.
    /// </summary>
    /// <returns>True if the game has been lost</returns>
    public bool getGameLostState()
    {
        return gameLost;
    }
    /// <summary>
    /// Finshes up the game and checks if an ad should be run
    /// </summary>
    public void lostGame()
    {
        if (ads.GetReady() || adFailedToLoad) 
        {
            bool result = ads.ShowInterstitialAd();
            //If the ad fails to load, it will run an ad after the next game,
            //despite where the timer is.
            if (!result)
            {
                adFailedToLoad = true;
            }
            else
            {
                adFailedToLoad = false;
            }
        }

        extraRound = false;
        gameReady = false;
        gameStarted = false;
        gameLost = true;
        scoreManager.gameOverScoreManagement();
    }

    public void RewardedAdPanelDisplayed()
    {
        gameLost = true;
        gameStarted = false;
    }

    public void ResetPlayedGames()
    {
        gamesPlayed = 0;
    }

    /// <summary>
    /// Begins the game
    /// </summary>
    public void startGame()
    {
        if (!ads.IsTimerRunning())
        {
            ads.StartCountdownTimer();
        }
        gameStarted = true;
        gamesPlayed++;
        spherePhysics.enableGravity();
    }

    public void StartRewardedAdRound()
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

    public void resetPlayerLocation()
    {
        playerControl.resetLocation();
    }
}
