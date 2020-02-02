using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public static float time;
    public int secondsUntilChime;
    public AudioSource chime;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Debug.Log(time);
        if(time > 1 && time%secondsUntilChime<1){
            if(!chime.isPlaying){
                chime.Play();
            }
        }
    }
}
