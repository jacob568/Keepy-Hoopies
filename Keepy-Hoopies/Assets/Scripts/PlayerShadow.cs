using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour {
    public GameObject player;
    private Vector3 newPosition;
    private float yPosition;


    // Use this for initialization
    void Start() {
        yPosition = 0.2f;

    }

    // Update is called once per frame.
    void FixedUpdate()
    {
        newPosition = new Vector3(player.transform.position.x, yPosition, player.transform.position.z);

        transform.position = newPosition;
    }
}
