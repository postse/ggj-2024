using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTerrain;
using Unity.VisualScripting;

public class TerrainScript : MonoBehaviour
{
    [SerializeField]
    private Texture2D OriginalTexture = null;

    [SerializeField]
    private BasicPaintableLayer CollidableLogicLayer;

    [SerializeField]
    private BasicPaintableLayer VisibleLayer;

    [SerializeField]
    private int width;

    [SerializeField]
    private int height;

    [SerializeField]
    private float smoothness;

    [SerializeField]
    private Color terrainColor;



    // Start is called before the first frame update
    void Start()
    {
        OriginalTexture = new PerlinTerrainGenerator().Generate(width: width, height: height, smoothness: smoothness, terrainColor: terrainColor);
        // OriginalTexture = new PerlinTerrainGenerator().Generate();

        VisibleLayer.OriginalTexture = OriginalTexture;
        CollidableLogicLayer.OriginalTexture = OriginalTexture;

        CollidableLogicLayer.InitLayer();
        VisibleLayer.InitLayer();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
