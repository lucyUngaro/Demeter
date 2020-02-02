using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientComponentManager : MonoBehaviour
{

    public Gradient gradient;
    // public Color[] colors;
    public Material mat;

    Color[] colors;
    float[] alphas;
    float[] colorAnchors;
    float[] alphaAnchors;

    void Start()
    {


        int colorSize = gradient.colorKeys.Length;
        int alphaSize = gradient.alphaKeys.Length;

        colors = new Color[colorSize];
        alphas = new float[alphaSize];
        colorAnchors = new float[colorSize];
        alphaAnchors = new float[alphaSize];


        for(int x = 0; x < colorSize; x++){
            colors[x] = gradient.colorKeys[x].color;
            colorAnchors[x] = gradient.colorKeys[x].time;
        }

        for(int x = 0; x < alphaSize; x++){
            alphas[x] = gradient.alphaKeys[x].alpha;
            alphaAnchors[x] = gradient.alphaKeys[x].time;
        }

        

        mat.SetColorArray("_Colors", colors);
        mat.SetFloatArray("_Alphas", alphas);
        mat.SetFloatArray("_ColorAnchors", colorAnchors);
        mat.SetFloatArray("_AlphaAnchors", alphaAnchors);

        mat.SetInt("_ColorCount", colorSize);
        mat.SetInt("_AlphaCount", alphaSize);
    }

    void Update()
    {
        int colorSize = gradient.colorKeys.Length;
        int alphaSize = gradient.alphaKeys.Length;

        colors = new Color[colorSize];
        alphas = new float[alphaSize];
        colorAnchors = new float[colorSize];
        alphaAnchors = new float[alphaSize];


        for(int x = 0; x < colorSize; x++){
            colors[x] = gradient.colorKeys[x].color;
            colorAnchors[x] = gradient.colorKeys[x].time;
        }

        for(int x = 0; x < alphaSize; x++){
            alphas[x] = gradient.alphaKeys[x].alpha;
            alphaAnchors[x] = gradient.alphaKeys[x].time;
        }

        

        mat.SetColorArray("_Colors", colors);
        mat.SetFloatArray("_Alphas", alphas);
        mat.SetFloatArray("_ColorAnchors", colorAnchors);
        mat.SetFloatArray("_AlphaAnchors", alphaAnchors);

        mat.SetInt("_ColorCount", colorSize);
        mat.SetInt("_AlphaCount", alphaSize);

    }
}
