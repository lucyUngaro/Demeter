using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptablePart : MonoBehaviour
{
    public void OnHit()
    {
        // Play destroy animation 

        GetComponentInParent<SculptureSequence>().OnPartDestroyed();
    }
}
