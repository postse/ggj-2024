using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinnerAnnouncement : MonoBehaviour
{

    public GameObject panel;
    public TextMeshProUGUI winnerText;

    public void DisplayEverything(CarController winner)
    {
        panel.SetActive(true);
        winnerText.text = "The winner of the game is " + winner.name + "!";
    }

    public void DisplayDraw()
    {
        panel.SetActive(true);
        winnerText.text = "This game will end in a draw!";
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
