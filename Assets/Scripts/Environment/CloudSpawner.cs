using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : NetworkBehaviour
{
    [SerializeField]
    private NetworkObject cloudPrefab;
    NetworkedTerrainGenerator terrainGenerator;

    void Start()
    {
        terrainGenerator = FindObjectOfType<NetworkedTerrainGenerator>();
        GenerateInitialClouds();
        StartCoroutine(RandomlyGenerateCloud());
    }

    void GenerateInitialClouds()
    {
        for (int i = 0; i < 5; i++)
        {
            NetworkObject nob = NetworkManager.GetPooledInstantiated(cloudPrefab, true);
            Spawn(nob);
        }
    }

    IEnumerator RandomlyGenerateCloud()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4.0f, 10.0f));
            CreateCloud();
        }
    }

    void CreateCloud()
    {
        Instantiate(cloudPrefab, new Vector3(terrainGenerator.width, Random.Range(terrainGenerator.height - 10, 50), 0), Quaternion.identity);
    }
}
