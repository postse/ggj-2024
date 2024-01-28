using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PerlinTerrainGenerator
{
    private int _width;
    private int _height;

    private float terrainOffsetRatio = .2f;

    public Texture2D Generate(float seed, int width = 3840, int height = 2160, float terrainSmoothness = 1000, Color? terrainColor = null, float textureSmoothness = 500)
    {
        _width = width;
        _height = height;

        // Tuple<terrainSmoothness, weight>
        // Smoothness in tuple is relative to initial smoothness value
        Tuple<float, float>[] floatTuples = new Tuple<float, float>[]
        {
            new Tuple<float, float>(1.0f, 1f),
            new Tuple<float, float>(.5f, .25f),
            new Tuple<float, float>(.25f, .125f),
        };
        
        Color[,] colorMap = new Color[width, height];
        for (int x = 0; x < width; x++)
        {

            var hillHeight = 0f;
            var totalHillHeight = 0f;

            for (int layer = 0; layer < floatTuples.Length; layer++)
            {
                hillHeight += Mathf.RoundToInt(GenerateHillHeight(x, terrainSmoothness * floatTuples[layer].Item1, seed) * floatTuples[layer].Item2);
                totalHillHeight += floatTuples[layer].Item2;
            }

            hillHeight /= totalHillHeight;
            hillHeight = Mathf.Min(hillHeight + (height * terrainOffsetRatio), height);

            for (int y = 0; y < height; y++)
            {
                if (y < hillHeight)
                {
                    // This could be much prettier. You can do that, I believe in you.
                    float brightness = Mathf.PerlinNoise(x / textureSmoothness + seed, y / textureSmoothness + seed);
                    brightness += Mathf.PerlinNoise(x / textureSmoothness*2 + seed, y / textureSmoothness*2 + seed) * 0.5f;
                    brightness += Mathf.PerlinNoise(x / textureSmoothness*4 + seed, y / textureSmoothness*4 + seed) * 0.25f;
                    brightness *= y/hillHeight;
                    brightness = Mathf.Clamp(brightness, 0.1f, 1.0f);
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
