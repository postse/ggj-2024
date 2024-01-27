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

    [Tooltip("Max health")]
    [SerializeField]
    private float maxHealth = 100f; // max fuel in seconds

    [SerializeField]
    private float health;

    [SerializeField]
    private float fuelConsumptionRate = 1.0f; // fuel consumed per second

    [SerializeField]
    private float bubblesRelativeFuelConsumptionRate = 2.0f; // fuel consumed per second

    [SerializeField]
    private float maxRotation = 80.0f; // max rotation in degrees

    public bool isTurn = false;
    public bool isDead = false;

    private float moveHorizontal;
    private bool isBubblesActive;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private TurnController turnController;
    bool flipped;

    public FuelBar fuelBar;
    public HealthBar healthBar;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        turnController = FindObjectOfType<TurnController>();
        fuel = maxFuel; // initialize fuel to maxFuel
        fuelBar.SetMaxFuel(maxFuel);
        health = maxHealth; // initialize health to maxHealth
        healthBar.SetMaxHealth(maxHealth);
        flipped = false;
    }

    void Update()
    {
        moveHorizontal = 0.0f;

        if (!isTurn) return;

        if (fuel > 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                moveHorizontal = -1.0f;
                fuel -= fuelConsumptionRate * Time.deltaTime; // consume fuel
                fuelBar.SetFuel(fuel); // change fuel bar
                if (flipped == false)
                {
                    FlipSprite(true);
                    flipped = true;
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                moveHorizontal = 1.0f;
                fuel -= fuelConsumptionRate * Time.deltaTime; // consume fuel
                fuelBar.SetFuel(fuel); // change fuel bar
                if (flipped == true)
                {
                    FlipSprite(false);
                    flipped = false;
                }
            }

            if (Input.GetButton("Jump"))
            {
                fuel -= fuelConsumptionRate * bubblesRelativeFuelConsumptionRate * Time.deltaTime; // consume fuel twice as fast
                fuelBar.SetFuel(fuel); // change fuel bar
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

    public void ResetFuel()
    {
        fuel = maxFuel;
        fuelBar.SetFuel(fuel);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) throw new ArgumentException("Damage must be positive");

        health -= Mathf.Min(damage, health);
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            isDead = true;
        }

        turnController.CheckIfGameOver();
    }

    public void Heal(float heal) {
        if (heal < 0) throw new ArgumentException("Heal must be positive");

        health += Mathf.Min(heal, health);
        healthBar.SetHealth(health);
    }

    public void ResetHealth()
    {
        health = maxHealth;
        healthBar.SetHealth(health);
    }
}
