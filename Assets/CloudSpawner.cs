using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject cloudPrefab;

    void Update()
    {
        int rand = Random.Range(0, 10000);

        if (rand > 50 && rand < 60)
        {
            CreateCloud();
        }

    }

    void CreateCloud()
    {
        Instantiate(cloudPrefab, new Vector3(110, Random.Range(0, 45), 0), Quaternion.identity);
    }
}
