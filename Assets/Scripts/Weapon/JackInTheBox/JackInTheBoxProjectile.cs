using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBoxProjectile : Projectile
{

    [SerializeField]
    private float explosionDelay = 5f;

    void Start() {
        Invoke("BeginExplode", explosionDelay);
    }

    void BeginExplode()
    {
        GetComponent<Animator>().SetTrigger("Explode");
        GetComponent<AudioSource>().Play();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        FindObjectOfType<CameraMovement>().TriggerShake();
    }
}
