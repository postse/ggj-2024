using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;

    [Tooltip("Max fuel in seconds")]
    [SerializeField]
    private float maxFuel = 5.0f; // max fuel in seconds

    [SerializeField]
    private float fuelConsumptionRate = 1.0f; // fuel consumed per second

    [SerializeField]
    private KeyCode moveLeftKey = KeyCode.A;

    [SerializeField]
    private KeyCode moveRightKey = KeyCode.D;

    private float moveHorizontal;
    private float fuel;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        fuel = maxFuel; // initialize fuel to maxFuel
    }

    void Update()
    {
        moveHorizontal = 0.0f;

        if (fuel > 0)
        {
            if (Input.GetKey(moveLeftKey))
            {
                moveHorizontal = -1.0f;
                fuel -= fuelConsumptionRate * Time.deltaTime; // consume fuel
            }
            else if (Input.GetKey(moveRightKey))
            {
                moveHorizontal = 1.0f;
                fuel -= fuelConsumptionRate * Time.deltaTime; // consume fuel
            }
        }

        fuel = Mathf.Clamp(fuel, 0, maxFuel); // ensure fuel is within [0, maxFuel]
    }

    void FixedUpdate()
    {
        if (moveHorizontal != 0.0f)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            Vector3 movement = rb.transform.up * moveHorizontal * speed;
            rb.velocity = new Vector2(movement.x, rb.velocity.y + Physics2D.gravity.y * Time.fixedDeltaTime);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
