using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour {
    public GameObject player;
    private Vector3 newPosition;
    private float yPosition;
    private Quaternion rotation;


    // Use this for initialization
    void Start() {
        rotation = transform.rotation;
        yPosition = 0.2f;

    }

    // Update is called once per frame.
    void Update()
    {
        newPosition = new Vector3(player.transform.position.x, yPosition, player.transform.position.z);

        transform.position = newPosition;
        transform.rotation = rotation;
    }
}
