using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPersistence {
    public static void SetMusicToggle(bool state)
    {
        // 0 is off, 1 is on
        int status = 0;
        if (state)
        {
            status = 1;
        }
        PlayerPrefs.SetInt("music", status);
    }

    public static void SetSoundToggle(bool state)
    {
        // 0 is off, 1 is on
        int status = 0;
        if (state)
        {
            status = 1;
        }
        PlayerPrefs.SetInt("sound", status);
    }

    public static int GetMusicStatus()
    {
        return PlayerPrefs.GetInt("music");
    }

    public static int GetSoundStatus()
    {
        return PlayerPrefs.GetInt("sound");
    }

    public static void SetHighScore(float score)
    {
        PlayerPrefs.SetFloat("highScore", score);
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.SetFloat("highScore", 0f);
    }

    public static float GetHighScore()
    {
        return PlayerPrefs.GetFloat("highScore");
    }

    public static int GetCoinCount()
    {
        return PlayerPrefs.GetInt("coins");
    }

    public static void SetCoinCount(int coins)
    {
        PlayerPrefs.SetInt("coins", coins);
    }
}
