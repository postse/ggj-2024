using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderConfiguration : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderValue;
    [SerializeField] private Slider slider;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private float defaultValue;
    [SerializeField] private bool isWholeNumber;

    public float Value => slider.value;
    public int IntValue => Mathf.RoundToInt(slider.value);

    // Start is called before the first frame update
    void Start()
    {
        var sliderStringFormat = isWholeNumber ? "0" : "0.0";

        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.wholeNumbers = isWholeNumber;

        slider.value = defaultValue;

        sliderValue.text = slider.value.ToString(sliderStringFormat);
        slider.onValueChanged.AddListener((value) =>
        {
            sliderValue.text = value.ToString(sliderStringFormat);
        });
    }
}
