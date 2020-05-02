using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAmbience : MonoBehaviour
{
    public AudioSource source;
    public static TimeAmbience timeAmbience;

    public AudioClip dayClip;
    public AudioClip nightClip;

    private bool dayTime = false;

    // Start is called before the first frame update
    void Start()
    {
        timeAmbience = this;   
    }

    public void playDaySound()
    {
        if (!dayTime)
        {
            dayTime = true;
            source.clip = dayClip;
            playSound();
        }
       
    }

    public void playNightSound()
    {
        if (dayTime)
        {
            source.clip = nightClip;
            playSound();
            dayTime = false;
        }
        
    }

    private void playSound()
    {
        source.Play();
    }
}
