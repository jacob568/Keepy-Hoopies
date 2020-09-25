using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelingSlowdown : MonoBehaviour {
    private Rigidbody rigidbody;
    private Vector3 newVelocity;
    private void OnTriggerStay(Collider other)
    {
        rigidbody = other.gameObject.GetComponentInParent<Rigidbody>();
        newVelocity = rigidbody.velocity;
        if (rigidbody.velocity.y > 0)
        {
            newVelocity.y -= .1f;
            rigidbody.velocity = newVelocity;
        }
    }
}
