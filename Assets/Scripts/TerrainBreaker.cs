using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using DTerrain;
using System.Linq;

public class TerrainBreaker : MonoBehaviour
{

    [SerializeField]
    private int craterSize = 64;

    [SerializeField]
    private float damage;

    private TerrainGenerator terrain;

    public Shape destroyCircle;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.Find("Terrain").GetComponent<TerrainGenerator>();
        destroyCircle = Shape.GenerateShapeCircle(craterSize);
    }


    public void BreakTerrain() {
        Vector3 p = this.transform.position;

        // Get all colliders within the craterSize radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(p, craterSize / terrain.CollidableLogicLayer.PPU);

        // Check if any of the colliders have the "Player" tag
        var hitPlayers = hitColliders.ToArray().Where(collider => collider.tag == "CarBody");

        // If there are players in the crater, damage them
        if (hitPlayers.Count() > 0)
        {
            foreach (var player in hitPlayers)
            {
                player.GetComponentInParent<CarController>().TakeDamage(damage);
            }
        }

        terrain.BreakTerrain(p, craterSize, destroyCircle);
    }
}
