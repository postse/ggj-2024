using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    void Start() {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "CarBody") {
            PickUp(collision.gameObject.transform.parent.gameObject);
        }
    }

    public virtual void PickUp(GameObject player) {
        Destroy(this.gameObject);
    }
}
