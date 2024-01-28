using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Good guide:
// https://medium.com/nice-things-ios-android-development/basic-2d-screen-shake-in-unity-9c27b56b516
public class ShakeBehavior : MonoBehaviour
{

    private Transform transform;
    private float shakeDuration = 0f;

    [SerializeField]
    private float shakeMagnitude = 0.7f;

    private float dampingSpeed = 1.0f;

    private Vector3 ogPosition;

    void Awake() {
        if (transform == null) {
            transform = GetComponent<Transform>();
        }
    }

    void OnEnable() {
        ogPosition = transform.localPosition;
    }

    void Update() {
        if (shakeDuration > 0) {
            transform.localPosition = ogPosition + Random.insideUnitSphere * shakeMagnitude;
   
            shakeDuration -= Time.deltaTime * dampingSpeed;
        } else {
            shakeDuration = 0f;
            transform.localPosition = ogPosition;
        }
    }
}
