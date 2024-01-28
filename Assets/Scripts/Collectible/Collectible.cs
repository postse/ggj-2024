using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.tag == "CarBody") {
            Debug.Log("Hit player!");
            PickUp(otherCollider.gameObject.transform.parent.gameObject);
        } else if (otherCollider.gameObject.tag == "Terrain") {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    public virtual void PickUp(GameObject player) {
        Destroy(this.gameObject);
    }
}
