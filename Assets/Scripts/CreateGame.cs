using UnityEngine;

public class CreateGame : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private int defaultPlayerCount;

    [SerializeField]
    private float edgeBuffer = 5f;

    void Awake()
    {
        try
        {
            var playerConfig = FindObjectOfType<PlayerConfig>();
            CreateGameWithPlayerCount(playerConfig.playerCount);
        }
        catch
        {
            CreateGameWithPlayerCount(defaultPlayerCount);
        }
    }

    public void CreateGameWithPlayerCount(int playerCount) 
    {
        var terrain = FindObjectOfType<TerrainGenerator>();

        var terrainPosition = terrain.gameObject.transform.position;
        float width = terrain.width - 10;
        float chunkWidth = width / playerCount;
        float chunkRandomness = width / (playerCount + 1);

        for (int i = 0; i < playerCount; i++)
        {
            // var randomX = Random.Range(terrainPosition.x + 5, terrainPosition.x + terrain.width - 5);

            // Put them in the dead center of their chunk
            float startX = terrainPosition.x + this.edgeBuffer + (chunkWidth * i) + (chunkWidth / 2);
            float randomOffset = Random.Range(-chunkRandomness / 2, chunkRandomness / 2);

            GameObject go = Instantiate(playerPrefab, new Vector3(startX + randomOffset, terrainPosition.y + terrain.height + 5, 0), Quaternion.identity);
            go.name = "Player " + (i + 1);
        }
    }
}
