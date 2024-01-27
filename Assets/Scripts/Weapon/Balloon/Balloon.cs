using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Balloon : Launchable
{

    void Start() {
        this.GetComponent<Rigidbody2D>().simulated = false;
    }

    public override void Launch(float angle, float power)
    {
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
        this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * power, ForceMode2D.Impulse);
        this.GetComponent<Rigidbody2D>().simulated = true;
    }

    // Physics stuff

    void Update() {
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f) {
            this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(this.GetComponent<Rigidbody2D>().velocity.y, this.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}
