using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnTurn : MonoBehaviour
{
    private CarController carController;
    private Vector3 startingScale;

    void Start()
    {
        carController = GetComponentInParent<CarController>();
        startingScale = transform.localScale;
    }

    void Update()
    {
        if (carController.isTurn)
        {
            transform.localScale = startingScale;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }
}
