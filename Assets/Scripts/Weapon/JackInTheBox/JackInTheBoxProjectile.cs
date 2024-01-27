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
        GetComponent<Animator>().SetTrigger("Explode");
    }

    public void BreakTerrain()
    {
        GetComponent<TerrainBreaker>().BreakTerrain();
    }

    public void DestroySelf() {
        Destroy(this.gameObject);
    }
}
