using UnityEngine;

public class GameSettings : MonoBehaviour
{
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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
