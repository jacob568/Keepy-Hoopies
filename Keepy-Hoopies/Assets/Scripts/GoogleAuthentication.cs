using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using TMPro;

public class GoogleAuthentication : MonoBehaviour
{
    public static PlayGamesPlatform platform;
    // Start is called before the first frame update
    void Start()
    {
        if (platform == null)
        {
            platform = PlayGamesPlatform.Activate();
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

        }
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Logged in successfully");
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }
}
