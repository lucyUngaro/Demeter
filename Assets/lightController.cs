using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour
{

    //lerps everything in its list to the color of the sun
    public Color sunrise, sunset, currentColor;
    public Gradient sunriseGrad, sunsetGrad, currentGradient;
    public List<SpriteRenderer> lightSprites = new List<SpriteRenderer>();
    public List<ParticleSystem> lightParts = new List<ParticleSystem>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lightSprites.Count; i++){
            lightSprites[i].color = Color.Lerp(lightSprites[i].color, currentColor, Time.deltaTime);
        }

        for (int i = 0; i < lightParts.Count; i++)
        {
            var main = lightParts[i].main;
            Color partColor = Color.Lerp(main.startColor.colorMin, currentColor, Time.deltaTime);
            main.startColor = new ParticleSystem.MinMaxGradient(partColor, partColor);
        }

        currentColor = Color.Lerp(sunrise, sunset, Mathf.PingPong(Time.time/15f, 1));
    }
}
