using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using DTerrain;

public class TerrainBreaker : MonoBehaviour
{

    [SerializeField]
    private int craterSize = 64;

    [SerializeField]
    private bool destroyOnImpact = false;

    private Terrain terrain;

    public Shape destroyCircle;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        destroyCircle = Shape.GenerateShapeCircle(craterSize);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Terrain") {
            if (destroyOnImpact) {
                BreakTerrain();
                Destroy(this.gameObject);
            }
        }
    }

    public void BreakTerrain() {
        Vector3 p = this.transform.position;

        terrain.BreakTerrain(p, craterSize, destroyCircle);
    }

}
