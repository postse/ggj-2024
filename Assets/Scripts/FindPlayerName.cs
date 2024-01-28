using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FindPlayerName : MonoBehaviour
{
    void Start()
    {
        var carController = GetComponentInParent<CarController>();
        var text = GetComponent<TextMeshProUGUI>();
        text.text = carController.name;
    }
}
