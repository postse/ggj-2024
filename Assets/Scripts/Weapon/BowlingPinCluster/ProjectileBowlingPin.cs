using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowlingPinProjectile : Projectile
{

    bool exploded = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exploded && (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("CarBody")))
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<SpriteRenderer>().sortingLayerName = "Effects";
            GetComponent<AudioSource>().Play();

            this.gameObject.transform.rotation = Quaternion.identity;
            exploded = true;
            FindObjectOfType<CameraMovement>().TriggerShake();
        }
    }
}
