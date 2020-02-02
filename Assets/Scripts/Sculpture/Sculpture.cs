using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : MonoBehaviour
{
    List<SculptureSequence> sequences = new List<SculptureSequence>();
    private int sculptureNum;
    private Manager manager;

    public void Init(int num, Manager man)
    {
        sculptureNum = num;
        manager = man;
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

                var sequence = child.gameObject.GetComponent<SculptureSequence>()   ;
                sequence.SetupParts(sequences.Count);
                sequences.Add(sequence);

                InitSequences(child);
            }
        }

    }

    public void SequenceComplete (SculptureSequence seq)
    {
        sequences.Remove(seq);
        CheckSculptureComplete();
    }

    private void CheckSculptureComplete ()
    {
        if (transform.childCount == 0 || (transform.childCount == 1 && transform.GetChild(0).gameObject.name == "DestroyMe")) // sculpture has been destroyed, add rubble
        {
            GameObject rubble = Instantiate(new GameObject(), SculptureData.GlobalSculptureData.transform.position, SculptureData.GlobalSculptureData.transform.rotation, transform);
            rubble.AddComponent<SpriteRenderer>().sprite = SculptureData.GlobalSculptureData.rubble[sculptureNum];

            // TODO: wait for button press
            manager.SendOutStatue();
        }

    }
}
