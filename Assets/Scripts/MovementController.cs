using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private float bubblesSpeed = 2;

    [Tooltip("Max fuel in seconds")]
    [SerializeField]
    private float maxFuel = 5.0f; // max fuel in seconds

    [SerializeField]
    private float fuel;

    [SerializeField]
    private float fuelConsumptionRate = 1.0f; // fuel consumed per second

    [SerializeField]
    private float bubblesRelativeFuelConsumptionRate = 2.0f; // fuel consumed per second

    [SerializeField]
    private float maxRotation = 80.0f; // max rotation in degrees

    private float moveHorizontal;
    private bool isBubblesActive;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private InputController inputController;
    bool flipped;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        inputController = GetComponent<InputController>();
        fuel = maxFuel; // initialize fuel to maxFuel
        flipped = false;
    }

    void Update()
    {
        moveHorizontal = 0.0f;

        if (fuel > 0)
        {
            if (inputController.GetAxis("Horizontal") < 0)
            {
                moveHorizontal = -1.0f;
                fuel -= fuelConsumptionRate * Time.deltaTime; // consume fuel
                if (flipped == false)
                {
                    FlipSprite(true);
                    flipped = true;
                }
            }
            else if (inputController.GetAxis("Horizontal") > 0)
            {
                moveHorizontal = 1.0f;
                fuel -= fuelConsumptionRate * Time.deltaTime; // consume fuel
                if (flipped == true)
                {
                    FlipSprite(false);
                    flipped = false;
                }
            }

            if (inputController.GetButton("Jump"))
            {
                fuel -= fuelConsumptionRate * bubblesRelativeFuelConsumptionRate * Time.deltaTime; // consume fuel twice as fast
                isBubblesActive = true;
            }
        }

        fuel = Mathf.Clamp(fuel, 0, maxFuel); // ensure fuel is within [0, maxFuel]
    }

    void FixedUpdate()
    {
        if (!isBubblesActive && moveHorizontal == 0.0f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        if (isBubblesActive)
        {
            rb.constraints = RigidbodyConstraints2D.None;

            rb.velocity = rb.transform.up * bubblesSpeed;
            isBubblesActive = false;
        }

        if (moveHorizontal != 0.0f)
        {
            rb.constraints = RigidbodyConstraints2D.None;

            Vector3 movement = rb.transform.right * moveHorizontal * movementSpeed;
            rb.velocity = new Vector2(movement.x, rb.velocity.y + Physics2D.gravity.y * Time.fixedDeltaTime);
        }

        float z = rb.transform.eulerAngles.z;
        if (z > 180.0f) z -= 360.0f; // Convert angle to [-180, 180] range
        z = Mathf.Clamp(z, -1 * maxRotation, maxRotation); // Clamp angle to [-80, 80] range
        rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, rb.transform.eulerAngles.y, z);
    }

    void FlipSprite(bool flip)
    {
        sprite.flipX = flip;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.CompareTag("Weapon") || sprite.CompareTag("Player"))
            {
                sprite.flipX = flip;
            }
        }
    }
}
