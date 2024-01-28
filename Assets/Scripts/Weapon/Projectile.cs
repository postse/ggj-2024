using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    [SerializeField]
    protected float destroyTimer = 14f;

    public void Launch(float angle, float power)
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * power, ForceMode2D.Impulse);
        
        // If it isn't destroyed by something else after 10s, destroy it
        Destroy(this.gameObject, destroyTimer);
    }

    public void BreakTerrain()
    {
        GetComponent<TerrainBreaker>().BreakTerrain();
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}