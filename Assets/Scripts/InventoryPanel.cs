using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryPanel : MonoBehaviour

{

    public InventoryManager inventory;
    //   public TextMeshPro BowlingPinsCount;
    //   public TextMeshPro JackInTheBoxCount;

    public TextMeshProUGUI BowlingPinsText;
    public TextMeshProUGUI JackInTheBoxText;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        int balloons = inventory.getItemCount(0);
        int bowlingPins = inventory.getItemCount(1);
        int jackInTheBox = inventory.getItemCount(2);

        //Debug.Log(BowlingPinsText.text);
        BowlingPinsText.text = bowlingPins.ToString();
        JackInTheBoxText.text = jackInTheBox.ToString();
    }
}
