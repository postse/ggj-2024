using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowlingPinCluster : Projectile
{

    [SerializeField]
    private float rotationPerSecond = 1.2f;
    [SerializeField]
    private float explosionDelay = 1.0f;
    [SerializeField]
    private GameObject bowlingPin;
    [SerializeField]
    private float spread = 15.0f;

    [SerializeField]
    private GameObject fizzleAnimation;
    bool exploded = false;

    void Start() {
        Invoke("Explode", explosionDelay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -360f * rotationPerSecond * Time.deltaTime);
    }

    void Explode() {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        float velTan = Mathf.Atan2(velocity.y, velocity.x);
        float velUpTan = velTan + spread * Mathf.PI / 180;
        Vector2 velUp = new Vector2(Mathf.Cos(velUpTan), Mathf.Sin(velUpTan)) * velocity.magnitude;
        float velDownTan = velTan - spread * Mathf.PI / 180;
        Vector2 velDown = new Vector2(Mathf.Cos(velDownTan), Mathf.Sin(velDownTan)) * velocity.magnitude;

        float velocityMag = velocity.magnitude;
        Vector3 offsetX = Vector3.right * (velocity.x > 0 ? 1 : -1);
        GameObject pin1 = Instantiate(bowlingPin, transform.position - offsetX, Quaternion.Euler(0, 0, velTan * Mathf.Rad2Deg + spread));
        pin1.GetComponent<Rigidbody2D>().velocity = velUp.normalized * velocityMag;
        
        GameObject pin2 = Instantiate(bowlingPin, transform.position, Quaternion.Euler(0, 0, velTan * Mathf.Rad2Deg));
        pin2.GetComponent<Rigidbody2D>().velocity = velocity.normalized * velocityMag;
        GameObject pin3 = Instantiate(bowlingPin, transform.position + offsetX, Quaternion.Euler(0, 0, velTan * Mathf.Rad2Deg - spread));
        
        pin3.GetComponent<Rigidbody2D>().velocity = velDown.normalized * velocityMag;
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exploded && collision.gameObject.CompareTag("Terrain"))
        {
            GameObject anim = Instantiate(fizzleAnimation, this.transform.position, Quaternion.identity);
            exploded = true;
            Destroy(anim, .6f);
            Destroy(this.gameObject);
        }
    }
}
