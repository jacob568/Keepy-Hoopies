using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpherePhysics : MonoBehaviour {
    public GameObject gameManager;
    private ScoreManager scoreManager;
    private Rigidbody sphere;
    private bool touchedRim;
    private Vector3 lastPosition;
    public GameObject ballTracker;
    private GameManagerScript gameManagerScript;
    private float ballDrops = 1;
    private bool gravityEnabled = true;

    private ConstantForce gravity;
    public float gravityForce;

    private bool ballTrackerActive;
    private TrailRenderer trail;

    private AudioSource audioSource;
    private AudioClip ballHitSound;
    public AudioClip scorePoint;
    private Vector3 centreLocation;

    private bool allowPoint;

    // Use this for initialization
    void Start () {
        allowPoint = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        trail = GetComponent<TrailRenderer>();
        trail.emitting = true;
        
        gravity = gameObject.AddComponent<ConstantForce>();
        gravity.force = new Vector3(0, gravityForce, 0);
        ballTrackerActive = true;
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        scoreManager = gameManager.GetComponent<ScoreManager>();
        sphere = GetComponent<Rigidbody>();
        touchedRim = false;
        centreLocation = new Vector3(0, 10f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManagerScript.getGameLostState())
        {
            gameObject.SetActive(false);
        }

        if (gravityEnabled)
        {
            gravity.force = new Vector3(0, gravityForce, 0);
            sphere.isKinematic = false;
        }
        else
        {
            sphere.isKinematic = true;
            gravity.force = Vector3.zero;
        }
        lastPosition = transform.position;
        

        if (ballTrackerActive)
        {
            ballTracker.SetActive(true);
        } else
        {
            ballTracker.SetActive(false);
        }


        //Testing
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(-9.0f, 10.0f, 6.6f);
            sphere.velocity = Vector3.zero;
        }

        //Testing
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(-93.4f, 75.4f, 48.1f);
            sphere.velocity = Vector3.zero;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
        //If the ball collides with player
        if (player)
        {
            //And if the ball has reached the height required to get another point
            if (scoreManager && allowPoint)
            {
                if (PlayerPersistence.GetSoundStatus() == 1)
                {
                    audioSource.clip = scorePoint;
                    audioSource.Play();
                }

                scoreManager.addToScore(1);
                allowPoint = false;
            }

            //This resets every time the player hits the ball.
            //It might be possible to get 10 points for a swish, if the ball bounces off
            //one hoop, and swishes into another.
            if (touchedRim)
            {
                touchedRim = false;
            }
        } else if (gameManagerScript.getGameState() && PlayerPersistence.GetSoundStatus() == 1)
        {
            audioSource.clip = ballHitSound;
            audioSource.Play();    
        }

        if (collision.gameObject.name == "Torus" || collision.gameObject.name == "Backboard")
        {
            SetTouchedRim(true);
        }

    }


    public void changeAllowPoint(bool value)
    {
        allowPoint = value;
    }

    /// <summary>
    /// Sets the value of the touched rim bool
    /// </summary>
    /// <param name="value">Value to set</param>
    public void SetTouchedRim(bool value)
    {
        touchedRim = value;
    }

    /// <returns>bool indicating if the ball has hit the rim of a hoop</returns>
    public bool getTouchedRim()
    {
        return touchedRim;
    }

    /// <summary>
    /// Registers a ball drop
    /// </summary>
    public void ballDropped()
    {
        ballDrops--;
    }

    /// <summary>
    /// Returns the ball to its starting position in the arena
    /// </summary>
    public void returnToStartPosition()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(0f, 10f, 0f);
        sphere.velocity = new Vector3(0f, 0f, 0f);
        ballDrops = 1;
    }


    /// <returns>The number of drops allowed before losing</returns>
    public float getBallDrops()
    {
        return ballDrops;
    }

    /// <summary>
    /// Toggles gravity for objects of this class
    /// </summary>
    public void toggleGravity()
    {
        gravityEnabled = !gravityEnabled;
    }

    /// <summary>
    /// Disables gravity for objects of this class.
    /// </summary>
    public void disableGravity()
    {
        gravityEnabled = false;

    }
    /// <summary>
    /// Enables gravity for objects of this class.
    /// </summary>
    public void enableGravity()
    {
        gravityEnabled = true;

    }

    public void centreBall()
    {
        trail.enabled = false;
        transform.position = centreLocation;
        sphere.velocity = Vector3.zero;
        toggleGravity();
        StartCoroutine(gravityTimer());
        
    }

    public Vector3 getMovementDirection()
    {
        Vector3 direction = (lastPosition - transform.position).normalized;
        return direction;
    }

    IEnumerator gravityTimer()
    {
        yield return new WaitForSeconds(2f);
        trail.enabled = true;
        toggleGravity();
    }
}
