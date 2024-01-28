using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryPanel : MonoBehaviour

{

    private TurnController turnController;

    public TextMeshProUGUI PlayerNameText;

    public TextMeshProUGUI BowlingPinsText;
    public TextMeshProUGUI JackInTheBoxText;

    // Start is called before the first frame update
    void Start()
    {
       turnController = FindObjectOfType<TurnController>();
    }

    public void SetPlayerNameText(string name)
    {
        PlayerNameText.text = name;
    }

    public void SetJackInTheBoxText(int count)
    {
        JackInTheBoxText.text = count.ToString();
    }

    public void SetBowlingPinsText(int count)
    {
        BowlingPinsText.text = count.ToString();
    }
}
