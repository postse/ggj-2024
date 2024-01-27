using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    public void Launch(float angle, float power)
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * power, ForceMode2D.Impulse);
    }

}