using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBoxProjectile : Projectile
{
    [SerializeField]
    private GameObject explosionAnimation;

    [SerializeField]
    private float explosionDelay = 5f;

    void Start() {
        Invoke("Explode", 5f);
    }

    void Explode() {
        GetComponent<TerrainBreaker>().BreakTerrain();
        GameObject anim = Instantiate(explosionAnimation, this.transform.position, Quaternion.identity);
        Destroy(anim, .6f);
        Destroy(this.gameObject);
    }
}
