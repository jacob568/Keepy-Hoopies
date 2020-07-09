using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelingSlowdown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
        Vector3 newVelocity = rigidbody.velocity;
        if (rigidbody.velocity.y > 0)
        {
            Debug.Log("APPLYING FORCE AAAAAA");
            newVelocity.y -= .05f;
            rigidbody.velocity = newVelocity;
        }
    }
}
