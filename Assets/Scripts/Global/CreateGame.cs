using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateGame : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private int defaultPlayerCount;

    [SerializeField]
    private float edgeBuffer = 5f;

    [SerializeField]
    private Sprite[] carSprites;

    private List<string> clownNames = new List<string>
    {
        "Bozo",
        "Ronald McDonald",
        "Krusty",
        "Joker",
        "Pogo",
        "Puddles",
        "Penny",
        "Patches",
        "Punchy",
        "Pennywise",
        "Penny",
        "Bingo",
        "Bongo",
        "Steve",
        "Scungo",
        "Abraham",
        "Denver",
        "Logan",
        "Simon",
        "Zane",
        "Bobby",
        "Bubbles",
        "ClownyMcClownface",
        "Oobabadooba"
    };

    void Awake()
    {
        try
        {
            var playerConfig = FindObjectOfType<GameSettings>();
            CreateGameWithPlayerCount(playerConfig.playerCount);
        }
        catch
        {
            CreateGameWithPlayerCount(defaultPlayerCount);
        }
    }

    private void Start()
    {
        var players = FindObjectsOfType<CarController>();
        foreach (var player in players)
        {
            int randomIndex = Random.Range(0, clownNames.Count);
            string randomClownName = clownNames[randomIndex];
            player.name = randomClownName;
            clownNames = clownNames.ToArray().Where((name, index) => index != randomIndex).ToList();
        }
    }

    public void CreateGameWithPlayerCount(int playerCount) 
    {
        var terrain = FindObjectOfType<TerrainGenerator>();

        var terrainPosition = terrain.gameObject.transform.position;
        float width = terrain.width - 10;
        float chunkWidth = width / playerCount;
        float chunkRandomness = width / (playerCount + 1);

        List<Sprite> carSpritesCopy = carSprites.ToList();

        int upperLimit = 3;

        for (int i = 0; i < playerCount; i++)
        {
            // var randomX = Random.Range(terrainPosition.x + 5, terrainPosition.x + terrain.width - 5);

            // Put them in the dead center of their chunk
            float startX = terrainPosition.x + this.edgeBuffer + (chunkWidth * i) + (chunkWidth / 2);
            float randomOffset = Random.Range(-chunkRandomness / 2, chunkRandomness / 2);

            GameObject go = Instantiate(playerPrefab, new Vector3(startX + randomOffset, terrainPosition.y + terrain.height + 5, 0), Quaternion.identity);

            int index = Random.Range(0, upperLimit);
            go.transform.Find("CarBody").GetComponent<SpriteRenderer>().sprite = carSpritesCopy[index];
            carSpritesCopy.RemoveAt(index);

            upperLimit -= 1;

            go.name = "Player " + (i + 1);
        }

        // Drop # of collectibles equal to number of players
        GetComponent<CollectibleController>().collectiblesPerDrop = playerCount;
    }
}
