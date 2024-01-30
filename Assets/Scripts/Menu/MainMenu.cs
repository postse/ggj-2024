using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    private BackgroundMusicController backgroundMusicController;
    public int playerCount;

    void Start() {
        backgroundMusicController = FindObjectOfType<BackgroundMusicController>();
    }

    public void PlayGame()
    {
        var playerConfig = FindObjectOfType<GameSettings>();
        backgroundMusicController.LowerVolume();
        
        SceneManager.LoadScene("Game");

        if (EventSystem.current.currentSelectedGameObject.name == "1player")
        {
            playerConfig.playerCount = 1;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "2players")
        {
            playerConfig.playerCount = 2;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "3players")
        {
            playerConfig.playerCount = 3;
        }
        else
        {
            playerConfig.playerCount = 4;
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
