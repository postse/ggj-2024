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
            exploded = true;
            GameObject anim = Instantiate(explosionAnimation, this.transform.position, Quaternion.identity);
            // TODO: Don't hardcode explosion time
            Destroy(anim, .6f);
            Destroy(this.gameObject);
        }
    }
}
