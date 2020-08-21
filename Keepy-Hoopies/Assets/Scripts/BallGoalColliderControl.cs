using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGoalColliderControl : MonoBehaviour {
    public GameObject sphere;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 colliderPosition = sphere.transform.position;
        colliderPosition.y += .4f;
        transform.position = colliderPosition;
        
	}
}
