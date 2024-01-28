using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

    [SerializeField]
    public bool isGameOver = false;

    CollectibleController collectibleController;
    InventoryPanel inventoryPanel;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        players[playerTurn].GetComponent<CarController>().isTurn = true;
        collectibleController = FindObjectOfType<CollectibleController>();
        inventoryPanel = FindObjectOfType<InventoryPanel>();


        // Initialize the inventory panel
        GetCurrentPlayer().GetComponent<InventoryManager>().SetInventoryPanel();
    }

    public void EndTurn()
    {
        players[playerTurn].GetComponent<CarController>().isTurn = false;
    }

    public GameObject GetCurrentPlayer()
    {
        return players[playerTurn];
    }

    public void SetNextPlayer()
    {
        Debug.Log("Setting next player");
        if (isGameOver) return;


        playerTurn++;
        if (playerTurn >= players.Length)
        {
            playerTurn = 0;
            roundNumber++;

            foreach (var player in players.Where(player => !player.GetComponent<CarController>().isDead))
            {
                player.GetComponent<CarController>().ResetFuel();

                // Only drop at end of round
                FindObjectOfType<CollectibleController>().DropCollectibles();
            }
        }

        var currentPlayer = players[playerTurn].GetComponent<CarController>();

        if (currentPlayer.isDead)
        {
            SetNextPlayer();
        } 
        else
        {
            currentPlayer.isTurn = true;
            GetCurrentPlayer().GetComponent<InventoryManager>().SetInventoryPanel();
        }
    }

    public void CheckIfGameOver()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        var livingPlayers = players.Where(player => !player.GetComponent<CarController>().isDead).ToList();
        if (livingPlayers.Count <= 1)
        {
            isGameOver = true;

            if (livingPlayers.Count == 1)
            {
                var winner = livingPlayers.First();
                Debug.Log("Player " + winner.GetComponent<CarController>().name + " wins!");
            }
            else
            {
                Debug.Log("Draw!");
            }
        }
    }
}
