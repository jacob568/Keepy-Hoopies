using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource music;
    private GameManagerScript gameManager;
    private bool musicPlaying = false;
    private bool musicPaused = false;

    // Use this for initialization
    void Start()
    {
        gameManager = GetComponent<GameManagerScript>();
        music = GetComponent<AudioSource>();
        music.loop = true;
        StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPersistence.GetMusicStatus() == 1)
        {
            if (!musicPlaying && gameManager.getGameState() && !gameManager.getPausedState())
            {
                PlayMusic();
                musicPlaying = true;
            }

            if (musicPlaying && gameManager.getGameLostState())
            {
                StopMusic();
                musicPlaying = false;
            }

            if (musicPlaying && gameManager.getGameState() && gameManager.getPausedState())
            {
                PauseMusic();
                musicPaused = true;
            }
        }
        else
        {
            StopMusic();
            musicPlaying = false;
        }

        if (musicPlaying && !gameManager.getGameState())
        {
            StopMusic();
        }


    }

    void PlayMusic()
    {
        music.Play();
    }

    void StopMusic()
    {
        music.Stop();
    }

    void PauseMusic()
    {
        music.Pause();
    }
}
