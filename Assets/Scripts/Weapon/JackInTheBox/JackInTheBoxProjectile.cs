using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBoxProjectile : Projectile
{

    [SerializeField]
    private AudioSource explosionSound;
    
    [SerializeField]
    private AudioSource popGoesTheWeasel;


    void Start() {
        popGoesTheWeasel.Play();
        Invoke("BeginExplode", popGoesTheWeasel.clip.length - 3f);
    }

    void BeginExplode()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Animator>().SetTrigger("Explode");
        StartCoroutine(ExplodeSound());
    }

    IEnumerator ExplodeSound()
    {
        FindObjectOfType<CameraMovement>().TriggerShake();
        yield return new WaitForSeconds(2.5f);
        explosionSound.Play();    
    }

}
