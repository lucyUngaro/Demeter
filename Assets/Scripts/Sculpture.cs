using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : MonoBehaviour
{

    List<SculptureSequence> sequences = new List<SculptureSequence>();

    private void Start()
    {
        InitSequences(transform);    
    }

    private void InitSequences (Transform parent)
    {
        // Go recursively through objects, adding the SculptureSequence scripts to all objects with children
        for (var i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);

            if (child.childCount > 0 && !child.gameObject.GetComponent<SpriteRenderer>()) // it's a parent which means it's a sequence
            {
                child.gameObject.AddComponent<SculptureSequence>();
                sequences.Add(child.gameObject.GetComponent<SculptureSequence>());

                InitSequences(child);
            }
        }

    }

    public void SequenceComplete (SculptureSequence seq)
    {
        sequences.Remove(seq);
    }
}
