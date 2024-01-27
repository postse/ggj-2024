using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using DTerrain;

public class TerrainBreaker : MonoBehaviour
{

    [SerializeField]
    private int size = 64;

    [SerializeField]
    private bool destroyOnImpact = false;

    public BasicPaintableLayer primaryLayer;
    public BasicPaintableLayer secondaryLayer;

    public Shape destroyCircle;

    // Start is called before the first frame update
    void Start()
    {
        destroyCircle = Shape.GenerateShapeCircle(size);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Terrain") {
            BreakTerrain();
        }
    }

    void BreakTerrain() {
        // Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition) - primaryLayer.transform.position;
        Vector3 p = this.transform.position - primaryLayer.transform.position;

        primaryLayer?.Paint(new PaintingParameters() 
        { 
            Color = UnityEngine.Color.clear, 
            Position = new Vector2Int((int)(p.x * primaryLayer.PPU) - size, (int)(p.y * primaryLayer.PPU) - size), 
            Shape = destroyCircle, 
            PaintingMode=PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.DESTROY
        });

        secondaryLayer?.Paint(new PaintingParameters() 
        {
            Color = UnityEngine.Color.clear,
            Position = new Vector2Int((int)(p.x * secondaryLayer.PPU) - size, (int)(p.y * secondaryLayer.PPU) - size), 
            Shape = destroyCircle, 
            PaintingMode=PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.NONE
        });

        if (destroyOnImpact) {
            Destroy(this.gameObject);
        }
    }
}
