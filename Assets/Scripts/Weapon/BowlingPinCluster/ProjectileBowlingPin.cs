using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionAnimation;

    bool exploded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exploded && collision.gameObject.CompareTag("Terrain"))
        {
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<AudioSource>().Play();

            exploded = true;
        }
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
