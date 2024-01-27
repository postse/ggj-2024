using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NoiseManager : MonoBehaviour
{
    public RawImage noiseTextureImage;

    private void Awake()
    {
        // fix this
        var terrainGenerator = new PerlinTerrainGenerator();
        noiseTextureImage.texture = terrainGenerator.Generate();
    }
}
