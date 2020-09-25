using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGoalColliderControl : MonoBehaviour {
    public GameObject sphere;
    private Vector3 colliderPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        colliderPosition = sphere.transform.position;
        colliderPosition.y += .4f;
        transform.position = colliderPosition;
	}
}
