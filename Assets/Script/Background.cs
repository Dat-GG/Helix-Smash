using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public UnityEngine.UI.RawImage img;

    private Texture2D backgroundTexture;

    void Awake()
    {
        backgroundTexture = new Texture2D(1, 2);
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;
        RandomColor(Random.ColorHSV(0, 1, .5f, 1, 1, 1), Random.ColorHSV(0, 1, .5f, 1, 1, 1));
    }

    public void RandomColor(Color color1, Color color2)
    {
        backgroundTexture.SetPixels(new Color[] { color1, color2, });
        backgroundTexture.Apply();
        img.texture = backgroundTexture;
    }
}
