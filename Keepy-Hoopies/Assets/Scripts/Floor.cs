using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    public GameManagerScript gameManagerScript;
    public UiManager uiManager;
    private AudioSource audioSource;
    public AudioClip loseLifeSound;
    public AudioClip gameOver;
    private bool extraRound;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();
        if (ball)
        {
            if (ball.getBallDrops() > 0)
            {
                ball.ballDropped();
                gameManagerScript.resetPlayerLocation();
                ball.centreBall();
                if (PlayerPersistence.GetSoundStatus() == 1)
                {
                    audioSource.clip = loseLifeSound;
                    audioSource.Play();
                }
            }
            else if (gameManagerScript.GetOfferAdState())
            {
                gameManagerScript.RewardedAdPanelDisplayed();
                if (PlayerPersistence.GetSoundStatus() == 1)
                {
                    audioSource.clip = loseLifeSound;
                    audioSource.Play();
                }
                StartCoroutine(uiManager.OpenRewardedAdPanel());
            }
            else
            {
                gameManagerScript.lostGame();
                if (PlayerPersistence.GetSoundStatus() == 1) {
                    audioSource.clip = gameOver;
                    audioSource.Play();
                }
            }
        }
    }
}
