using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10.0f;

    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    private float moveHorizontal;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveHorizontal = 0.0f;

        if (Input.GetKey(moveLeftKey))
        {
            moveHorizontal = -1.0f;
        }
        else if (Input.GetKey(moveRightKey))
        {
            moveHorizontal = 1.0f;
        }
    }

    void FixedUpdate()
    {
        Debug.Log(rb.transform.up);
        Vector3 movement = rb.transform.up * moveHorizontal * speed;
        Debug.Log(movement);

        if (moveHorizontal != 0.0f)
        {
            rb.velocity = new Vector2(movement.x, movement.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
