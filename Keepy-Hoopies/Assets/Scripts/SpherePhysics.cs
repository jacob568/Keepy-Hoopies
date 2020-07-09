using System.Collections;
using System.Collections.Generic;
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

    private AudioSource audioSource;
    private AudioClip ballHitSound;
    public AudioClip scorePoint;
    private Vector3 centreLocation;
    private TrailRenderer trail;
    private bool cooldown = false;
    private MeshRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<MeshRenderer>();
        trail = GetComponent<TrailRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        //trail = GetComponent<TrailRenderer>();
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            renderer.material.shader = Shader.Find("_Color");
            renderer.material.SetColor("_Color", Color.green);

            //Find the Specular shader and change its Color to red
            renderer.material.shader = Shader.Find("Specular");
            renderer.material.SetColor("_SpecColor", Color.red);
        }
        if (gameManagerScript.getGameLostState())
        {
            gameObject.SetActive(false);
        }
        if (gravityEnabled)
        {
            gravity.force = new Vector3(0, gravityForce, 0);
        } else
        {
            gravity.force = Vector3.zero;
        }
        lastPosition = transform.position;

        //if (gameManagerScript.getGameState())
        //{
        //    ballTrackerActive = gameManagerScript.getGameState();
        //}

        if (ballTrackerActive)
        {
            ballTracker.SetActive(true);
        } else
        {
            ballTracker.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(-7.6f, 10.0f, 6.6f);
            sphere.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(-93.4f, 75.4f, 48.1f);
            sphere.velocity = Vector3.zero;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();

        
        if (player && !cooldown)
        {
            if (scoreManager)
            {
                if (PlayerPersistence.GetSoundStatus() == 1)
                {
                    audioSource.clip = scorePoint;
                    audioSource.Play();
                }

                scoreManager.addToScore(1);

                StartCoroutine(collisionCooldown());
                cooldown = true;

            }
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

    public void SetTouchedRim(bool touched)
    {
        touchedRim = touched;
    }

    public bool getTouchedRim()
    {
        return touchedRim;
    }

    public void ballDropped()
    {
        ballDrops--;
    }

    public void returnToCannon()
    {
        cooldown = false;
        gameObject.SetActive(true);
        transform.position = new Vector3(0f, 10f, 0f);
        sphere.velocity = new Vector3(0f, 0f, 0f);
        ballDrops = 1;
    }

    public float getBallDrops()
    {
        return ballDrops;
    }

    public void toggleGravity()
    {
        gravityEnabled = !gravityEnabled;
    }

    public void centreBall()
    {
        trail.emitting = false;
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

    IEnumerator collisionCooldown()
    {
        yield return new WaitForSeconds(.3f);
        cooldown = false;
    }

    IEnumerator gravityTimer()
    {
        yield return new WaitForSeconds(1f);
        trail.emitting = true;
        toggleGravity();
    }
}
