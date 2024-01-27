using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinProjectile : Projectile
{
    [SerializeField]
    private GameObject explosionAnimation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;

        }
    }
}
