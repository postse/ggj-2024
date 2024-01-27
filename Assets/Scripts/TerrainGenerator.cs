using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTerrain;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.GameCenter;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    private Texture2D CollisionTexture = null;

    [SerializeField]
    private Texture2D VisualTexture = null;

    [SerializeField]
    public BasicPaintableLayer CollidableLogicLayer;

    [SerializeField]
    private BasicPaintableLayer VisibleLayer;

    public int width;

    public int height;

    [SerializeField]
    private float smoothness;

    [SerializeField]
    private Color terrainColor;



    // Start is called before the first frame update
    void Start()
    {
        var pixelWidth = width * CollidableLogicLayer.PPU;
        var pixelHeight = height * CollidableLogicLayer.PPU;

        CollisionTexture = CollisionTexture == null ? new PerlinTerrainGenerator().Generate(pixelWidth, pixelHeight, smoothness, terrainColor) : CollisionTexture;
        VisualTexture = VisualTexture == null ? CollisionTexture : VisualTexture;

        VisibleLayer.OriginalTexture = VisualTexture;
        CollidableLogicLayer.OriginalTexture = CollisionTexture;

        CollidableLogicLayer.InitLayer();
        VisibleLayer.InitLayer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BreakTerrain(Vector3 center, int size, Shape destroyShape) {
        center -= CollidableLogicLayer.transform.position;

        CollidableLogicLayer?.Paint(new PaintingParameters() 
        { 
            Color = UnityEngine.Color.clear, 
            Position = new Vector2Int((int)(center.x * CollidableLogicLayer.PPU) - size, (int)(center.y * CollidableLogicLayer.PPU) - size), 
            Shape = destroyShape, 
            PaintingMode=PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.DESTROY
        });

        VisibleLayer?.Paint(new PaintingParameters() 
        {
            Color = UnityEngine.Color.clear,
            Position = new Vector2Int((int)(center.x * VisibleLayer.PPU) - size, (int)(center.y * VisibleLayer.PPU) - size), 
            Shape = destroyShape, 
            PaintingMode=PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.NONE
        });

    }
}
