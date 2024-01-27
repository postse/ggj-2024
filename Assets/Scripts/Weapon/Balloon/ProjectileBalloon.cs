using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ProjectileBalloon : Projectile {
    [SerializeField]
    private GameObject explosionAnimation;

    bool exploded = false;

    void Update() {
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f) {
            this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(this.GetComponent<Rigidbody2D>().velocity.y, this.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    void BreakTerrain() {
        GetComponent<TerrainBreaker>().BreakTerrain();
    }

    void DestroySelf() {
        Destroy(this.gameObject);
    }
}
