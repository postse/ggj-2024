using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Rigidbody2D parentTransform;
    private CarController carController;
    private float startingXOffset;
    private float startingYOffset;

    void Start()
    {
        parentTransform = GetComponentInParent<Rigidbody2D>();
        carController = GetComponentInParent<CarController>();
        startingXOffset = transform.position.x - parentTransform.position.x;
        startingYOffset = transform.position.y - parentTransform.position.y;

        // Lock rotation
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        //var flippedInt = carController.flipped ? -1 : 1;
        transform.position = new Vector3(parentTransform.position.x + startingXOffset, parentTransform.position.y + startingYOffset, transform.position.z);
        transform.rotation = Quaternion.identity;
    }
}
