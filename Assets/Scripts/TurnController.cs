using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField]
    private int roundNumber = 1;

    // 0 indexed since it's using an array
    [SerializeField]
    private int playerTurn = 0;

    [SerializeField]
    private GameObject[] players;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        players[playerTurn].GetComponent<CarController>().isTurn = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Change GetButton to GetButtonDown
        {
            players[playerTurn].GetComponent<CarController>().isTurn = false;

            playerTurn++;
            if (playerTurn >= players.Length)
            {
                playerTurn = 0;
                roundNumber++;
            }

            players[playerTurn].GetComponent<CarController>().isTurn = true;
        }
    }
}
