using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] collectibles;

    private int counter = 0;

    void Update() {
        counter++;
        // if (Input.GetKeyDown(KeyCode.H)) {
        if (counter % 100 == 0) {
            int index = Random.Range(0, collectibles.Length-1);
            Debug.Log(index);
            int randX = Random.Range(0, 100);
            int randY = 50;
            Vector3 position = new Vector3(randX, randY, 0);
            Quaternion rotation = new Quaternion();
            DropCollectible(collectibles[index], position, rotation);
        }
    }

    public void DropCollectible(GameObject collectiblePrefab, Vector3 position, Quaternion rotation) {
        GameObject collectible = Instantiate(collectiblePrefab, position, rotation);
    }
}
