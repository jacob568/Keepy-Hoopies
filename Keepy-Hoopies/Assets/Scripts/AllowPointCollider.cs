using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowPointCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        SpherePhysics sphere = other.gameObject.GetComponent<SpherePhysics>();
        if (sphere)
        {
            sphere.changeAllowPoint(true);
        }
    }
}
