using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraInset = 2f;
    private GameSettings gameSettings;

    void Start()
    {
        gameSettings = FindObjectOfType<GameSettings>();
        var parent = GetComponentInParent<TerrainGenerator>();

        float desiredWidth = (gameSettings?.mapWidth ?? parent.width); // The desired width of the camera's viewport in world units

        var terrainPosition = parent.transform.position;

        // Calculate the orthographic size based on the desired width and the aspect ratio
        Camera.main.orthographicSize = desiredWidth / (2 * Camera.main.aspect) - (cameraInset / 2);

        // Position the camera so that the bottom left of the view is at (0,0)
        float cameraX = Camera.main.orthographicSize * Camera.main.aspect; // half of the horizontal viewing area
        Camera.main.transform.position = new Vector3(terrainPosition.x + cameraX + cameraInset, terrainPosition.y + Camera.main.orthographicSize + cameraInset, -10); // Adjust the Z value as needed
        
        ogPosition = cameraTransform.localPosition;
    }


    // Good guide:
    // https://medium.com/nice-things-ios-android-development/basic-2d-screen-shake-in-unity-9c27b56b516
    private Transform cameraTransform;
    private float shakeDuration = 0f;

    [SerializeField]
    private float defaultShakeDuration = 0.1f;

    [SerializeField]
    private float shakeMagnitude = 0.7f;

    private float dampingSpeed = 1.0f;

    private Vector3 ogPosition;

    void Awake() {
        if (cameraTransform == null) {
            cameraTransform = GetComponent<Transform>();
        }
    }

    void Update() {
        if (shakeDuration > 0) {
            cameraTransform.localPosition = ogPosition + Random.insideUnitSphere * shakeMagnitude;
   
            shakeDuration -= Time.deltaTime * dampingSpeed;
        } else {
            shakeDuration = 0f;
            cameraTransform.localPosition = ogPosition;
        }
    }

    public void TriggerShake() {
        shakeDuration = defaultShakeDuration;
    }
}