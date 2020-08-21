using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelingSlowdown : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
        Vector3 newVelocity = rigidbody.velocity;
        if (rigidbody.velocity.y > 0)
        {
            newVelocity.y -= .1f;
            rigidbody.velocity = newVelocity;
        }
    }
}
