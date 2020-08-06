﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button playButton, leaderboardButton, achievementsButton, helpButton, closeHelpMenu, settingsButton;
    public Toggle swapControls, musicToggle, soundToggle;
    public TitleSettingsPositioner settings;
    public TextMeshProUGUI scoreText;
    public GameObject helpPanel;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
        helpButton.onClick.AddListener(OpenHelpPanel);
        closeHelpMenu.onClick.AddListener(CloseHelpPanel);
        playButton.onClick.AddListener(StartGame);
        achievementsButton.onClick.AddListener(OpenAchievementsPanel);
        leaderboardButton.onClick.AddListener(OpenLeaderboardPanel);
        settingsButton.onClick.AddListener(ToggleSettingsPanel);
        scoreText.text = PlayerPersistence.GetHighScore().ToString();

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

        soundToggle.isOn = PlayerPersistence.GetSoundStatus() == 1 ? true : false;
        musicToggle.isOn = PlayerPersistence.GetMusicStatus() == 1 ? true : false;
        swapControls.isOn = PlayerPersistence.GetFlipControlStatus() == 1 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ToggleSettingsPanel()
    {
        if (settings.GetIsOpen())
        {
            settings.ClosePanel();
        }
        else
        {
            settings.OpenPanel();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OpenAchievementsPanel()
    {
        Social.ShowAchievementsUI();
    }

    private void OpenLeaderboardPanel()
    {
        Social.ShowLeaderboardUI();
    }

    private void OpenHelpPanel()
    {
        helpPanel.SetActive(true);
    }

    private void CloseHelpPanel()
    {
        helpPanel.SetActive(false);
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


}
