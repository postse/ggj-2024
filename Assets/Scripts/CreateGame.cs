using UnityEngine;

public class CreateGame : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private int defaultPlayerCount;

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

        for (int i = 0; i < playerCount; i++)
        {
            var randomX = Random.Range(terrainPosition.x + 5, terrainPosition.x + terrain.width - 5);
            Instantiate(playerPrefab, new Vector3(randomX, terrainPosition.y + terrain.height + 5, 0), Quaternion.identity);
        }
    }
}
