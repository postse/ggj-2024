using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinProjectile : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Point the object in the direction of its velocity
        transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity);
    }
}
