using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PerlinTerrainGenerator : MonoBehaviour
{
    private int _width;
    private int _height;

    public Texture2D Generate(int width = 3840, int height = 2160, float smoothness = 500, Color? groundColor = null)
    {
        _width = width;
        _height = height;

        // fix this
        var seed = Random.Range(0, 10);
        Color[,] colorMap = new Color[width, height];
        for (int x = 0; x < width; x++)
        {
            var hillHeight = Mathf.RoundToInt(height * Mathf.PerlinNoise(x / smoothness + seed, 0));

            for (int y = 0; y < height; y++)
            {
                if (y < hillHeight)
                {
                    colorMap[x, y] = groundColor ?? Color.green;
                }
                else
                {
                    colorMap[x, y] = Color.clear;
                }
            }
        }
        return _SetNoiseTexture(colorMap);
    }

    private Texture2D _SetNoiseTexture(Color[,] colorMap)
    {
        Color[] pixels = new Color[_width * _height];

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                pixels[x + _width * y] = colorMap[x, y];
            }
        }

        Texture2D texture = new Texture2D(_width, _height);
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}
