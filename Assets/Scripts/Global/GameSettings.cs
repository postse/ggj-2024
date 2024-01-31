using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    public int playerCount = 2;
    public float smoothness = 500;
    public int mapWidth = 3840;
    public int mapHeight = 2160;
    public float maxFuel = 5;
    public float maxHealth = 100;
    public float dropsMultiplier = 1;
    public float movementSpeed = 5;
    public float weaponPower = 1;
    public float weaponPowerCycleSpeed = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
