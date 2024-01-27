using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTerrain;
using Unity.VisualScripting;

public class TerrainScript : MonoBehaviour
{
    [SerializeField]
    public Texture2D OriginalTexture;

    [SerializeField]
    BasicPaintableLayer CollidableLogicLayer;

    [SerializeField]
    BasicPaintableLayer VisibleLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Testing 1 2 3");
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
