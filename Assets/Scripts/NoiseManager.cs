using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CreatePerlinTerrain : MonoBehaviour
{
    private int _width;
    private int _height;
    private Color _groundColor;

    public Texture2D Generate(int width = 3840, int height = 2160, float smoothness = 10000, Color? groundColor = null)
    {
        _width = width;
        _height = height;
        _groundColor = groundColor ?? Color.green;

        // fix this
        var seed = Random.Range(0, 10);
        float[,] noise = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            var hillHeight = Mathf.RoundToInt(height * Mathf.PerlinNoise(x / smoothness + seed, 0));

            for (int y = 0; y < height; y++)
            {
                if (y < hillHeight)
                {
                    noise[x, y] = 1;
                }
                else
                {
                    noise[x, y] = 0;
                }
            }
        }
        return _SetNoiseTexture(noise);
    }

    private Texture2D _SetNoiseTexture(float[,] noise)
    {
        Color[] pixels = new Color[_width * _height];

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                pixels[x + _width * y] = Color.Lerp(Color.clear, _groundColor, noise[x, y]);
            }
        }

        Texture2D texture = new Texture2D(_width, _height);
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}
