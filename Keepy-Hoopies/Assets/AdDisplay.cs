using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdDisplay : MonoBehaviour
{

    public string myGameIdAndroid = "";
    public string videoPlacement = "";
    public bool adStarted;
    private bool testMode = true;
    ShowOptions options = new ShowOptions();
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(myGameIdAndroid, testMode);
    }

    public bool ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            return true;
        }
        return false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
