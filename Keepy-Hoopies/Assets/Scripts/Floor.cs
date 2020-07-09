using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    public GameManagerScript gameManagerScript;
    private AudioSource audioSource;
    public AudioClip loseLifeSound;
    public AudioClip gameOver;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        SpherePhysics ball = collision.gameObject.GetComponent<SpherePhysics>();
        if (ball)
        {
            if (ball.getBallDrops() > 0)
            {
                ball.ballDropped();
                ball.centreBall();
                if (PlayerPersistence.GetSoundStatus() == 1)
                {
                    audioSource.clip = loseLifeSound;
                    audioSource.Play();
                }
            } else
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
