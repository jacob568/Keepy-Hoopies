using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdDisplay : MonoBehaviour, IUnityAdsListener
{
    public string myGameIdAndroid = "3731455";
    public string rewardedVideo = "rewardedVideo";
    private bool testMode = true;
    private Button yesButton;
    private CountdownTimer countdownTimer;

    public UiManager uiManager;
    public GameManagerScript gameManager;
    public BallController ball;
    // Start is called before the first frame update
    void Start()
    {
        countdownTimer = gameManager.GetComponent<CountdownTimer>();
        yesButton = GetComponent<Button>();
        yesButton.interactable = Advertisement.IsReady(myGameIdAndroid);

        if (yesButton)
        {
            Debug.Log("added listener");
            yesButton.onClick.AddListener(ShowRewardedVideo);
        }

        Advertisement.AddListener(this);
        Advertisement.Initialize(myGameIdAndroid, testMode);
    }

    public bool IsAdReady()
    {
        OnUnityAdsReady(rewardedVideo);
        return yesButton.interactable;
    }

    public void ShowRewardedVideo()
    {
        gameManager.SetExtraRound();
        Advertisement.Show(rewardedVideo);
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == rewardedVideo)
        {
            yesButton.interactable = true;
        }
        else
        {
            yesButton.interactable = false;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            gameManager.ads.ResetAdInterval();
            gameManager.gameLost = false;
            StartCoroutine(uiManager.CloseRewardedAdPanel());
            countdownTimer.refresh();
            countdownTimer.IsRewardedAdRound();
            gameManager.resetPlayerLocation();
            gameManager.ResetPlayedGames();
            ball.gameObject.SetActive(true);
            ball.disableGravity();
            ball.centreBallNoGravityToggle();
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            gameManager.lostGame();
            StartCoroutine(uiManager.CloseRewardedAdPanel());
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
            StartCoroutine(uiManager.CloseRewardedAdPanel());
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
