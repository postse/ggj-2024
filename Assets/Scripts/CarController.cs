using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.Examples;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    public string name;

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
    private float bubblesFuelUsed = 2.0f; // fuel consumed per second

    [SerializeField]
    private float maxRotation = 80.0f; // max rotation in degrees
    [SerializeField]
    private AudioSource carSound;

    public bool isTurn = false;
    public bool isDead = false;

    private float moveHorizontal;
    private bool isBubblesActive;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField]
    private SpriteRenderer clownSpriteRenderer;
    private Color ogColor;
    private Color ogClownColor;
    private TurnController turnController;
    private InventoryManager inventoryManager;
    private GameLogic gameLogic;
    public bool flipped;



    public FuelBar fuelBar;
    public HealthBar healthBar;

    private bool engineSoundRunning = false;

    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        rb = GetComponentInChildren<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        ogColor = sprite.color;
        ogClownColor = clownSpriteRenderer.color;
        turnController = FindObjectOfType<TurnController>();
        inventoryManager = GetComponent<InventoryManager>();
        fuel = (gameLogic?.maxFuel ?? maxFuel); // initialize fuel to maxFuel
        maxFuel = (gameLogic?.maxFuel ?? maxFuel);
        fuelBar.SetMaxFuel(maxFuel);
        health = (gameLogic?.maxHealth ?? maxHealth); // initialize health to maxHealth
        healthBar.SetMaxHealth((gameLogic?.maxHealth ?? maxHealth));
        flipped = false;
        movementSpeed = (gameLogic?.movementSpeed ?? movementSpeed);
    }

    void Update()
    {
        moveHorizontal = 0.0f;

        if (!isTurn || turnController.isGameOver) return;

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

            // Play engine sound if moving
            if (Input.GetAxis("Horizontal") != 0) {
                if (!engineSoundRunning) {
                    carSound.Play();
                    engineSoundRunning = true;
                    StartCoroutine(StopSound());
                }
            } else {
                engineSoundRunning = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                // Variable jump height somewhat based upon fuel before jump
                var fuelBeforeBubble = fuel;
                fuel -=  Mathf.Min(fuel, bubblesFuelUsed); // consume fuel twice as fast
                fuelBar.SetFuel(fuel); // change fuel bar
                rb.constraints = RigidbodyConstraints2D.None;

                rb.AddForce(Vector2.up * 100 * bubblesSpeed * (Mathf.Min(fuelBeforeBubble, bubblesFuelUsed) / bubblesFuelUsed));
            }

            if (Input.GetButtonDown("CycleProjectile")) {
                inventoryManager.CycleProjectile();
            }

            if (Input.GetKeyDown(KeyCode.B)) {
            //    FindObjectOfType<ShakeBehavior>().TriggerShake();
               FindObjectOfType<CameraMovement>().TriggerShake();
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

        if (moveHorizontal != 0.0f)
        {
            rb.constraints = RigidbodyConstraints2D.None;

            Vector3 movement = rb.transform.right * moveHorizontal * movementSpeed;
            rb.velocity = new Vector2(movement.x, rb.velocity.y + Physics2D.gravity.y * Time.fixedDeltaTime);

            var rigidBodySize = rb.GetComponent<SpriteRenderer>().bounds.size.x;

            // Ensure the rigidbody does not leave the camera bounds
            float cameraWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
            float cameraLeftEdge = Camera.main.transform.position.x - cameraWidth / 2;
            float cameraRightEdge = Camera.main.transform.position.x + cameraWidth / 2;
            float objectHalfWidth = rigidBodySize / 2;

            float clampedX = Mathf.Clamp(rb.position.x, cameraLeftEdge + objectHalfWidth, cameraRightEdge - objectHalfWidth);
            rb.position = new Vector2(clampedX, rb.position.y);
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

    public void AddFuel(float fuelAmt)
    {
        if (fuel < 0) throw new ArgumentException("Fuel must be positive");

        fuel = Mathf.Min(fuel + fuelAmt, maxFuel);
        fuelBar.SetFuel(fuel);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) throw new ArgumentException("Damage must be positive");

        Blink();    // Flicker player color
        health = Mathf.Max(health - damage, 0);

        if (health <= 0)
        {
            isDead = true;
        }
        
        healthBar.SetHealth(health);
        turnController.CheckIfGameOver();
    }

    public void Heal(float heal) {
        if (heal < 0) throw new ArgumentException("Heal must be positive");

        health = Mathf.Min(health + heal, maxHealth);
        healthBar.SetHealth(health);
    }

    public void ResetHealth()
    {
        health = maxHealth;
        healthBar.SetHealth(health);
    }

    /* Sound based functions */
    private IEnumerator StopSound()
    {
        
        while (engineSoundRunning)
        {
            yield return new WaitForSeconds(0.25f);
        }

        carSound.Stop();
    }
    
    private void SetColor(Color color, Color clownColor) {
        sprite.color = color;
        clownSpriteRenderer.color = clownColor;
    }

    // Flicker player color to Red
    public void Blink() {
        Color damageColor = new Color(100, 0, 0);

        SetColor(damageColor, damageColor);

        StartCoroutine(ResetColor());

        IEnumerator ResetColor()
        {
            yield return new WaitForSecondsRealtime(0.2f);
            SetColor(ogColor, ogClownColor);
        }
    }
}
