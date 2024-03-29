using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    [SerializeField]
    protected Transform firePoint;
    [SerializeField]
    protected Transform idlePoint;

    [SerializeField]
    protected float angle = 45;

    [SerializeField]
    protected float powerMultiplier = 50;

    public float currentPower = 0f;
    private bool powerIncreasing = true;

    [SerializeField]
    private float powerCycleSpeed = 3f;

    [SerializeField]
    private float minAngle = -135;
    [SerializeField]
    private float maxAngle = 135;

    [SerializeField]
    private GameObject idlePrefab;
    private GameObject _idleObject;

    [SerializeField]
    protected GameObject projectilePrefab;

    [SerializeField]
    private float _aimSpeed = 45f;

    [SerializeField]
    private InventoryManager inventoryManager;
    private CarController carController;
    private GameLoop gameLoop;
    private GameSettings gameSettings;

    public bool shotWeapon = false;

    public PowerBar powerBar;

    void Start() {
        gameSettings = GameSettings.Instance;
        powerCycleSpeed = (gameSettings?.weaponPowerCycleSpeed ?? powerCycleSpeed);
        powerMultiplier = (gameSettings?.weaponPower ?? powerMultiplier);
        carController = GetComponentInParent<CarController>();
        powerBar.SetMaxPower(1);
        powerBar.SetPower(0);
        gameLoop = FindObjectOfType<GameLoop>();
    }

    void Update()
    {
        if (!carController.isTurn) return;

        if (Input.GetButton("Fire1"))
        {
            float powerIncrement = (1f / powerCycleSpeed) * Time.deltaTime;

            if (powerIncreasing)
            {
                currentPower += powerIncrement;

                if (currentPower >= 1f)
                {
                    currentPower = 1f;
                    powerIncreasing = false;
                }
            }
            else
            {
                currentPower -= powerIncrement;

                if (currentPower <= 0f)
                {
                    currentPower = 0f;
                    powerIncreasing = true;
                }
            }
            powerBar.SetPower(currentPower);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            Launch();
            currentPower = 0;
            powerBar.SetPower(currentPower);
            inventoryManager.SpendItem();
        }

        if (Input.GetButton("AimLeft"))
        {
            SetAim(1);
        }
        else if (Input.GetButton("AimRight"))
        {
            SetAim(-1);
        }
    }

    public void SetAim(float angleDelta) {
        float newAngle = this.angle + angleDelta * _aimSpeed * Time.deltaTime;
        if (newAngle > maxAngle) {
            this.angle = maxAngle;
        } else if (newAngle < minAngle) {
            this.angle = minAngle;
        } else {
            this.angle = newAngle;
        }
        this.UpdateTransforms();
    }

    IEnumerator DisableClownCollision()
    {
        var clownSpriteRenderer = transform.Find("Clown").gameObject.GetComponent<EdgeCollider2D>();

        clownSpriteRenderer.enabled = false;

        yield return new WaitForSeconds(.2f);

        clownSpriteRenderer.enabled = true;
    }

    public void Launch()
    {
        StartCoroutine(LaunchCoroutine());
        StartCoroutine(DisableClownCollision());
    }

    private IEnumerator LaunchCoroutine()
    {
        gameLoop.EndTurn();
        Destroy(_idleObject);
        if (!projectilePrefab) yield break;  // Projectile is null, don't launch

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Arrow is offset by 90 degrees
        var projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.Launch(this.transform.parent.rotation.eulerAngles.z + angle + 90f, currentPower * powerMultiplier);
        while (GameObject.FindGameObjectsWithTag("Projectile").Length > 0)
        {
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(.5f);

        gameLoop.SetNextPlayer();
    }

    protected void UpdateTransforms()
    {
        firePoint.rotation = Quaternion.Euler(0, 0, angle + this.transform.parent.rotation.eulerAngles.z);
    }

    public void SetIdleSprite(GameObject idlePrefab) {
        Destroy(_idleObject);   // Clear current idle object
        _idleObject = Instantiate(idlePrefab, idlePoint.position, idlePoint.rotation);
        _idleObject.transform.SetParent(idlePoint);
        this.UpdateTransforms();
    }

    public void SetProjectilePrefab(GameObject projectilePrefab) {
        this.projectilePrefab = projectilePrefab;
    }

}
