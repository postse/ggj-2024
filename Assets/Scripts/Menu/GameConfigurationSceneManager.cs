using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameConfigurationSceneManager : MonoBehaviour
{
    private BackgroundMusicController backgroundMusicController;
    private GameSettings gameSettings;

    [SerializeField] private SliderConfiguration mapSizeSlider;
    [SerializeField] private SliderConfiguration hillHeightSlider;
    [SerializeField] private SliderConfiguration maxHealthSlider;
    [SerializeField] private SliderConfiguration maxFuelSlider;
    [SerializeField] private SliderConfiguration movementSpeed;
    [SerializeField] private SliderConfiguration weaponPower;
    [SerializeField] private SliderConfiguration powerBarDifficulty;

    void Start()
    {
        backgroundMusicController = FindObjectOfType<BackgroundMusicController>();
        gameSettings = GameSettings.Instance;
    }

    public void ToGame()
    {
        if (backgroundMusicController != null) backgroundMusicController.LowerVolume();

        gameSettings.mapWidth = mapSizeSlider.IntValue * 50;
        gameSettings.mapHeight = Mathf.RoundToInt(mapSizeSlider.IntValue * 50 * .3f);
        gameSettings.smoothness = (mapSizeSlider.IntValue * 50f * 5f) / hillHeightSlider.Value;
        gameSettings.playerCount = 3;
        gameSettings.maxHealth = maxHealthSlider.IntValue;
        gameSettings.maxFuel = maxFuelSlider.Value;
        gameSettings.movementSpeed = movementSpeed.Value;
        gameSettings.weaponPower = weaponPower.Value;
        gameSettings.weaponPowerCycleSpeed = 3 / powerBarDifficulty.Value;

        SceneManager.LoadScene("Game");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
