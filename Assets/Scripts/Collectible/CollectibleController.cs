using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] collectibles;

    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            int index = Random.Range(0, collectibles.Length-1);
            Debug.Log(index);
            int randX = Random.Range(0, 100);
            int randY = 50;
            Vector3 position = new Vector3(randX, randY, 0);
            Quaternion rotation = new Quaternion();
            DropCollectible(collectibles[index], position, rotation);
            Debug.Log("Dropped item");
        }
    }

    public void DropCollectible(GameObject collectiblePrefab, Vector3 position, Quaternion rotation) {
        GameObject collectible = Instantiate(collectiblePrefab, position, rotation);
    }
}
