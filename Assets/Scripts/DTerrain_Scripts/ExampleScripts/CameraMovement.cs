using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void Start()
    {
        var parent = GetComponentInParent<TerrainGenerator>();

        float desiredWidth = parent.width; // The desired width of the camera's viewport in world units

        var terrainPosition = parent.transform.position;

        // Calculate the orthographic size based on the desired width and the aspect ratio
        Camera.main.orthographicSize = desiredWidth / (2 * Camera.main.aspect);

        // Position the camera so that the bottom left of the view is at (0,0)
        float cameraX = Camera.main.orthographicSize * Camera.main.aspect; // half of the horizontal viewing area
        Camera.main.transform.position = new Vector3(terrainPosition.x + cameraX, terrainPosition.y + Camera.main.orthographicSize, -10); // Adjust the Z value as needed
    }
}