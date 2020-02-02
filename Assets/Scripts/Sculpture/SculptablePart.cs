using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptablePart : MonoBehaviour
{
    private void Start()
    {
        transform.position = SculptureData.GlobalSculptureData.transform.position; 
        transform.rotation = SculptureData.GlobalSculptureData.transform.rotation;
        transform.localScale = SculptureData.GlobalSculptureData.transform.scale;
    }

    public void OnHit()
    {
        // Play destroy animation
        Destroy(gameObject);
        GetComponentInParent<SculptureSequence>().OnPartDestroyed();
    }

}
