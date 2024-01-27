using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowlingPinCluster : MonoBehaviour
{

    [SerializeField]
    private float rotationPerSecond = 1.2f;
    [SerializeField]
    private float explosionDelay = 1.0f;
    [SerializeField]
    private GameObject bowlingPin;
    [SerializeField]
    private float spread = 15.0f;

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
        GameObject pin1 = Instantiate(bowlingPin, transform.position, Quaternion.Euler(0, 0, velTan * Mathf.Rad2Deg + spread));
        pin1.GetComponent<Rigidbody2D>().velocity = velUp;
        GameObject pin2 = Instantiate(bowlingPin, transform.position, Quaternion.Euler(0, 0, velTan * Mathf.Rad2Deg));
        pin2.GetComponent<Rigidbody2D>().velocity = velocity;
        GameObject pin3 = Instantiate(bowlingPin, transform.position, Quaternion.Euler(0, 0, velTan * Mathf.Rad2Deg - spread));
        pin3.GetComponent<Rigidbody2D>().velocity = velDown;
        Destroy(this.gameObject);
    }
}
