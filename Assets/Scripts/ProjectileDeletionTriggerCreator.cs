using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeletionTriggerCreator : MonoBehaviour
{
    private TurnController turnController;

    void Start()
    {
        turnController = FindObjectOfType<TurnController>();
        var parent = GetComponentInParent<TerrainGenerator>();

        var terrainPosition = parent.transform.position;

        // Create a BoxCollider2D
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

        // Set the size of the BoxCollider2D to match the terrain's width and height
        boxCollider.size = new Vector2(parent.width, parent.height * 20);

        // Set the position of the BoxCollider2D to match the terrain's bottom left corner
        boxCollider.offset = new Vector2(terrainPosition.x + parent.width / 2f, terrainPosition.y + boxCollider.size.y / 2f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("CarBody"))
        {
            other.GetComponentInParent<CarController>().isDead = true;
            if (other.GetComponentInParent<CarController>().isTurn)
            {
                turnController.EndTurn();
                turnController.SetNextPlayer();
            }
        }
    }
}
