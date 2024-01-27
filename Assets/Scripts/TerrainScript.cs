using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTerrain;
using Unity.VisualScripting;

public class TerrainScript : MonoBehaviour
{

    [SerializeField]
    BasicPaintableLayer CollidableLogicLayer;

    [SerializeField]
    BasicPaintableLayer VisibleLayer;

    [SerializeField]
    public Texture2D OriginalTexture;

    // Start is called before the first frame update
    void Start()
    {
        VisibleLayer.OriginalTexture = OriginalTexture;
        CollidableLogicLayer.OriginalTexture = OriginalTexture;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
