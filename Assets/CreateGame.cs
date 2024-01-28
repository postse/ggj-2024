using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGame : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private TerrainGenerator terrain;

    private void Awake()
    {
        var playerConfig = FindObjectOfType<PlayerConfig>().GetComponent<PlayerConfig>();
        CreateGameWithPlayerCount(playerConfig.playerCount);
    }

    public void CreateGameWithPlayerCount(int playerCount) 
    {


        var terrainPosition = terrain.gameObject.transform.position;

        for (int i = 0; i < playerCount; i++)
        {
            var randomX = Random.Range(terrainPosition.x + 5, terrainPosition.x + terrain.width - 5);
            Instantiate(playerPrefab, new Vector3(randomX, terrainPosition.y + terrain.height + 5, 0), Quaternion.identity);
        }
    }
}
