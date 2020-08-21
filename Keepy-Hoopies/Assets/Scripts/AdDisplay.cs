using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdDisplay : MonoBehaviour
{
    public string myGameIdAndroid = "3731455";
    public string videoPlacement = "video";

    private bool testMode = true;
    private bool readyToDisplayAd;
    private bool timerRunning;
    private float adInterval;
    private float intervalLength;
    // Start is called before the first frame update
    void Start()
    {
        intervalLength = 420f;
        timerRunning = false;
        adInterval = intervalLength;
        readyToDisplayAd = false;
        Advertisement.Initialize(myGameIdAndroid, testMode);
    }

    /// <returns>if the ad timer is running</returns>
    public bool IsTimerRunning()
    {
        return timerRunning;
    }

    public bool ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            readyToDisplayAd = false;
            return true;
        }
        return false;
        
    }

    /// <summary>
    /// A cycling timer that indicates when to display an ad.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AdCountdown()
    {
        timerRunning = true;
        while (adInterval > 0)
        {
            adInterval--;
            yield return new WaitForSeconds(1f);
        }
        readyToDisplayAd = true;
        adInterval = intervalLength;
        timerRunning = false;
    }

    /// <summary>
    /// Starts the countdown timer
    /// </summary>
    public void StartCountdownTimer()
    {
        StartCoroutine(AdCountdown());
    }


    /// <returns>if ad is ready to be displayed</returns>
    public bool GetReady()
    {
        return readyToDisplayAd;
    }

    public void ResetAdInterval()
    {
        adInterval = intervalLength;
    }
}
