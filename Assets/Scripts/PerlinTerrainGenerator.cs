using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PerlinTerrainGenerator : MonoBehaviour
{
    private int _width;
    private int _height;

    public Texture2D Generate(int width = 3840, int height = 2160, float terrainSmoothness = 500, Color? terrainColor = null, float textureSmoothness = 500)
    {
        _width = width;
        _height = height;

        // Tuple<terrainSmoothness, weight>
        // Smoothness in tuple is relative to initial smoothness value
        Tuple<float, float>[] floatTuples = new Tuple<float, float>[]
        {
            new Tuple<float, float>(1.0f, 1f),
            new Tuple<float, float>(.5f, .1f),
            new Tuple<float, float>(.2f, .05f)
        };
        
        // Texture Noise
        
        var seed = UnityEngine.Random.value * 100;
        Color[,] colorMap = new Color[width, height];
        for (int x = 0; x < width; x++)
        {

            var hillHeight = 0f;
            var totalHillHeight = 0f;

            for (int y = 0; y < floatTuples.Length; y++)
            {
                hillHeight += Mathf.RoundToInt(GenerateHillHeight(x, terrainSmoothness * floatTuples[y].Item1, seed) * floatTuples[y].Item2);
                totalHillHeight += floatTuples[y].Item2;
            }

            hillHeight /= totalHillHeight;

            for (int y = 0; y < height; y++)
            {
                if (y < hillHeight)
                {
                    float brightness = Mathf.PerlinNoise(x / textureSmoothness + seed, y / textureSmoothness + seed);
                    Color color = terrainColor ?? Color.green;
                    color *= brightness;
                    color.a = 1.0f;
                    colorMap[x, y] = color;
                }
                else
                {
                    colorMap[x, y] = Color.clear;
                }
            }
        }
        return _SetNoiseTexture(colorMap);
    }

    private float GenerateHillHeight(int x, float terrainSmoothness, float seed)
    {
        return _height * Mathf.PerlinNoise(x / terrainSmoothness + seed, 0);
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
