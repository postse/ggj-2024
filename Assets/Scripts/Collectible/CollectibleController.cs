using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] collectibles;

    [SerializeField]
    private int spawnHeight = 55;

    [SerializeField]
    public int collectiblesPerDrop = 5;


    public void DropCollectibles() {
        for (int i = 0; i < collectiblesPerDrop; i++) {
            DropCollectible();
        }
    }

    public void DropCollectible() {
        int index = Random.Range(0, collectibles.Length);
        int randX = Random.Range(5, 95);
        int randY = spawnHeight;
        Vector3 position = new Vector3(randX, randY, 0);
        GameObject collectible = Instantiate(collectibles[index], position, Quaternion.identity);
    }
}
