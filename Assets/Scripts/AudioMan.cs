using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMan : MonoBehaviour
{

    public AudioSource source;
    public List<AudioClip> audioList = new List<AudioClip>();
 

    public void playRandomSound()
    {
        AudioClip sound = audioList[Random.Range(0, audioList.Count - 1)];

        source.clip = sound;
        source.Play();
    }

    
}
