using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject cloudPrefab;
    TerrainGenerator terrainGenerator;



    void Start()
    {
        terrainGenerator = FindObjectOfType<TerrainGenerator>();
        GenerateInitialClouds();
        StartCoroutine(RandomlyGenerateCloud());
    }

    void GenerateInitialClouds()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(0, terrainGenerator.width), Random.Range(terrainGenerator.height - 10, 50), 0), Quaternion.identity);
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
