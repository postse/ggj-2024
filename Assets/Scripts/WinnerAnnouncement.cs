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
        winnerText.text = "" + winner.name + " Wins!";
        winnerText.outlineWidth = 0.5f;
        winnerText.outlineColor = new Color32(0, 0, 0, 255);
    }

    public void DisplayDraw()
    {
        panel.SetActive(true);
        winnerText.text = "This game ends in a draw!";
        winnerText.outlineWidth = 0.2f;
        winnerText.outlineColor = new Color32(0, 0, 0, 255);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
