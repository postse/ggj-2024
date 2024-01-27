using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NoiseManager : MonoBehaviour
{
    public RawImage noiseTextureImage;

    public int width = 10000;
    public int height = 10000;
    public float smoothness = 100000;

    private void Awake()
    {
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
        _SetNoiseTexture(noise);
    }

    private void _SetNoiseTexture(float[,] noise)
    {
        Color[] pixels = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                pixels[x + width * y] = Color.Lerp(Color.clear, Color.green, noise[x, y]);
            }
        }

        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(pixels);
        texture.Apply();
        noiseTextureImage.texture = texture;
    }
}
