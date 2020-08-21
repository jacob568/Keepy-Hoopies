using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopCheck : MonoBehaviour {
    public GameObject gameManager;
    private ScoreManager scoreManager;
    public GameObject sphere;
    private BallController spherePhysics;
    public  AudioSource audioSource;
    public AudioClip nonSwishSound;
    public AudioClip swishSound;
    public ParticleSystem confetti;
    private bool isColliding = false;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        scoreManager = gameManager.GetComponent<ScoreManager>();
        spherePhysics = sphere.GetComponent<BallController>();
	}
	
	// Update is called once per frame

    private void OnTriggerEnter(Collider hoopTrigger)
    {
        BallGoalColliderControl ballCollider = hoopTrigger.gameObject.GetComponent<BallGoalColliderControl>();
        if (ballCollider)
        {
            // Ensures points are only given if the ball goes through the top of the hoop
            if (spherePhysics.getMovementDirection().y > 0)
            {
                // More points given if the sides of the hoop aren't touched
                if (!spherePhysics.getTouchedRim())
                {
                    // iscolliding is required so it doesn't trigger multiple times
                    isColliding = true;
                    if (PlayerPersistence.GetSoundStatus() == 1)
                    {
                        audioSource.clip = swishSound;
                        audioSource.Play();
                    }
                    scoreManager.scoreSwish();
                    confetti.Play();
                    wait(.5f);
                }
                // if hoop was touched
                else
                {
                    isColliding = true;
                    if (PlayerPersistence.GetSoundStatus() == 1)
                    {
                        audioSource.clip = nonSwishSound;
                        audioSource.Play();
                    }
                    scoreManager.scoreHoop();
                    confetti.Play();
                    spherePhysics.SetTouchedRim(false);
                    wait(.5f);
                }

                spherePhysics.centreBall();
            }
        }
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isColliding = false;
    }
}
