using UnityEngine;

public class LeftJoystickPlayerController : MonoBehaviour
{
    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    public Transform rotationTarget; // the game object that will rotate to face the input direction
    public float moveSpeed = 8f; // movement speed of the player character
    public int rotationSpeed = 8; // rotation speed of the player character
    private Rigidbody rigidBody; // rigid body component of the player character
    private Vector3 leftJoystickInput; // holds the input of the Left Joystick
    float xMovementLeftJoystick; // The horizontal movement from joystick 01
    float zMovementLeftJoystick; // The vertical movement from joystick 01

    private Vector3 previousLocation;
    private Vector3 newLocation;
    private int layerMask = 1 << 9;

    void Start()
    {
        newLocation = transform.position;
        previousLocation = Vector3.zero;
        if (transform.GetComponent<Rigidbody>() == null)
        {
            Debug.LogError("A RigidBody component is required on this game object.");
        }
        else
        {
            rigidBody = transform.GetComponent<Rigidbody>();
        }

        if (leftJoystick == null)
        {
            Debug.LogError("The left joystick is not attached.");
        }

        if (rotationTarget == null)
        {
            Debug.LogError("The target rotation game object is not attached.");
        }
    }

    void LateUpdate()
    {
        //if (newLocation != previousLocation)
        //{
        //    Vector3 direction = (newLocation - previousLocation).normalized;

        //    Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        //    transform.rotation = rotation;
        //}
        //Vector3 temp = Vector3.zero;

        //temp.x += xMovementLeftJoystick;
        //temp.z += zMovementLeftJoystick;

        //Vector3 lookDirection = (temp - transform.position).normalized;
        //Debug.Log("position: " + transform.position);
        //Debug.Log("temp: " + temp);
        //Debug.Log("Look direction: " + lookDirection);

        //if (lookDirection != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0f, 0f, 0f);
        //    //rotationTarget.localRotation = Quaternion.Slerp(rotationTarget.localRotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
        //    //rotationTarget.localRotation = new Quaternion(0f, rotationTarget.localRotation.y, 0f, 0f);
        //}
    }

    void Update()
    {
        // get input from both joysticks
        leftJoystickInput = leftJoystick.GetInputDirection();

        xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

        // if there is only input from the left joystick
        if (leftJoystickInput != Vector3.zero)
        {
            // calculate the player's direction based on angle
            float tempAngle = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
            xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
            zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

            leftJoystickInput = new Vector3(xMovementLeftJoystick, 0, zMovementLeftJoystick);
            leftJoystickInput = transform.TransformDirection(leftJoystickInput);
            leftJoystickInput *= moveSpeed;
            if (!Physics.Raycast(transform.position, leftJoystickInput, 14f, layerMask))
            {
                transform.Translate(leftJoystickInput * moveSpeed * Time.deltaTime, Space.World); 
            }


            // move the player
            //rigidBody.transform.Translate(leftJoystickInput * Time.fixedDeltaTime);
        }
    }
}