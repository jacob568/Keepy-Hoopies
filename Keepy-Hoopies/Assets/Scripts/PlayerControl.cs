using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
    private Vector3 touchOrigin = -Vector3.one;
    private float movementSpeed = 4f;
    private Rigidbody playerRigidbody;
    public float jumpForce;
    private bool isJumping;
    private bool isGrounded;
    private float moveHorizontal;
    private float moveVertical;
    public AudioClip jumpSound;
    private AudioSource audioSource;
    private Vector3 direction;
    private Vector3 directionNormalized;
    private float magnitude;

    private Vector3 newLocation;
    private Vector3 previousLocation;

    public Button jumpButton;

    //Mobile
    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    private Vector3 joystickIdlePosition;
    public Transform joystickHandle;
    private Vector3 leftJoystickInput; // holds the input of the Left Joystick
    float xMovementLeftJoystick; // The horizontal movement from joystick 01
    float zMovementLeftJoystick; // The vertical movement from joystick 01
    Vector3 centreJoystick;

    // Use this for initialization
    void Start () {
        centreJoystick = joystickHandle.position;
        newLocation = transform.position;
        previousLocation = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody>();
        isJumping = false;
        audioSource.clip = jumpSound;
        jumpButton.onClick.AddListener(jump);
    }


    // Update is called once per frame
    void Update ()
    {
        LayerMask layerMask = LayerMask.GetMask("Wall");
        previousLocation = transform.position;
        Vector3 moveDirection = Vector3.zero;
        /**
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (!Physics.Raycast(transform.position, movement, 14f, layerMask))
        {
            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }       **/
//#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        leftJoystickInput = leftJoystick.GetInputDirection();
        moveDirection = new Vector3(leftJoystickInput.x, 0f, leftJoystickInput.y);
        xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

        direction = (joystickHandle.position - centreJoystick);
        magnitude = direction.magnitude / 10f;
        directionNormalized = direction.normalized;
        directionNormalized.z = directionNormalized.y;
        directionNormalized.y = 0f;


        // if there is touch input
        if (leftJoystickInput != Vector3.zero)
        {
            // player is rotated to direction of movement
            Quaternion rotation = Quaternion.LookRotation(moveDirection, Vector3.up) * Quaternion.Euler(0f, 90f, 0f);
            Quaternion extraRotation = Quaternion.Euler(30f, 0, 30f);
            transform.rotation = rotation;
            // these are used to check in a cone in front of the player.
            // this helps prevent clipping through walls.
            Vector3 leftPosition = Quaternion.AngleAxis(-30, Vector3.up) * directionNormalized;
            Vector3 rightPosition = Quaternion.AngleAxis(30, Vector3.up) * directionNormalized;

            //COllision detection
            if (!Physics.Raycast(transform.position, leftJoystickInput, 1.3f, layerMask)
                && !Physics.Raycast(transform.position, leftPosition, 1.3f, layerMask)
                && !Physics.Raycast(transform.position, rightPosition, 1.3f, layerMask))
            {
                transform.Translate(directionNormalized * magnitude * movementSpeed * Time.deltaTime, Space.World);
            }
        }
//#endif
    }

    // Method which handles player jumping
    private void jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            isGrounded = false;
            playerRigidbody.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
            if (PlayerPersistence.GetSoundStatus() == 1)
            {
                audioSource.Play();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    // moves player to the central position
    public void resetLocation()
    {
        transform.position = new Vector3(0f, 1.7f, 0f);
    }
}