using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptablePart : MonoBehaviour
{
    private void Start()
    {
        transform.position = SculptureData.GlobalSculptureData.position; 
        transform.rotation = SculptureData.GlobalSculptureData.rotation;
        transform.localScale = SculptureData.GlobalSculptureData.scale;
    }

    public void OnHit()
    {
        // Play destroy animation 

        GetComponentInParent<SculptureSequence>().OnPartDestroyed();
    }
}
