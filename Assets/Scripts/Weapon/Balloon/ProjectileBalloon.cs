using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ProjectileBalloon : Projectile {
    [SerializeField]
    private GameObject explosionAnimation;

    void Update() {
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f) {
            this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(this.GetComponent<Rigidbody2D>().velocity.y, this.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            GameObject anim = Instantiate(explosionAnimation, this.transform.position, Quaternion.identity);
            
            // TODO: Don't hardcode explosion time
            Destroy(anim, .6f);
            Destroy(this.gameObject);
        }
    }

    public override void Launch(float angle, float power)
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * power, ForceMode2D.Impulse);
    }
}
