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
        Invoke("BeginExplode", 5f);
    }

    void BeginExplode()
    {
        GetComponent<Animator>().Play("JackBox");
        Invoke("EndExplosion", 1);
    }

    void EndExplosion()
    {
        GetComponent<TerrainBreaker>().BreakTerrain();
        Destroy(this.gameObject);
    }
}
