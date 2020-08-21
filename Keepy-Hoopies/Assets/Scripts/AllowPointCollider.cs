using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowPointCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        BallController sphere = other.gameObject.GetComponent<BallController>();
        if (sphere)
        {
            sphere.changeAllowPoint(true);
        }
    }
}
