using System.Linq;
using UnityEngine;

public class GameLoop : MonoBehaviour
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
    
    [SerializeField]
    private WinnerAnnouncement winnerAnnouncement;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        players[playerTurn].GetComponent<CarController>().isTurn = true;

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
        if (isGameOver) return;


        playerTurn++;
        if (playerTurn >= players.Length)
        {
            playerTurn = 0;
            roundNumber++;

            foreach (var player in players.Where(player => !player.GetComponent<CarController>().isDead))
            {
                player.GetComponent<CarController>().ResetFuel();
            }

            // Only drop at end of round
            FindObjectOfType<CollectibleController>().DropCollectibles();
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

            #if UNITY_EDITOR
                // Weird issue when running in the editor where the winner announcement is null when the editor exits which causes an annoying error
                if (winnerAnnouncement == null) return;
            #endif

            if (livingPlayers.Count == 1)
            {
                var winner = livingPlayers.First().GetComponent<CarController>();
                winnerAnnouncement.gameObject.SetActive(true);
                winnerAnnouncement.DisplayEverything(winner);
            }
            else
            {
                Debug.Log("Draw!");
                winnerAnnouncement.gameObject.SetActive(true);
                winnerAnnouncement.DisplayDraw();
            }
        }
    }
}
