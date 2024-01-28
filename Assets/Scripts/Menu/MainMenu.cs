using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public int playerCount;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        int loadPlayers;
        if (EventSystem.current.currentSelectedGameObject.name == "1player") {
            playerCount = 1;
        } else if (EventSystem.current.currentSelectedGameObject.name == "2players") {
            playerCount = 2;
        } else if (EventSystem.current.currentSelectedGameObject.name == "3players") {
            playerCount = 3;
        } else {
            playerCount = 4;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
