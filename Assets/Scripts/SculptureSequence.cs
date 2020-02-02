using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptureSequence : MonoBehaviour
{

    private List<SculptablePart> parts = new List<SculptablePart>();
    private SculptablePart part;

    // Start is called before the first frame update
    void Start()
    {
        InitParts();
        GetNextPart();
    }

    private void InitParts()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var childObj = transform.GetChild(i).gameObject;

            if (childObj.GetComponent<SpriteRenderer>()) // it's a sprite, which means it's a statue part
            {
                var polygonCollider = childObj.AddComponent<PolygonCollider2D>();
                polygonCollider.isTrigger = true;

                var sculptablePart = childObj.GetComponent<SculptablePart>();
                
                if (!sculptablePart)
                {
                    sculptablePart = childObj.AddComponent<SculptablePart>();
                }

                parts.Add(sculptablePart); // add to the parts array
            }

        }
    }

    public void OnPartDestroyed()
    {
        parts.Remove(part);
        Destroy(part.gameObject);
   
        part = GetNextPart();
        CheckIfSequenceComplete();
    }
    
    void CheckIfSequenceComplete()
    {
        if (part == null)
        {
            Debug.Log("SEQUENCE COMPLETE");

            gameObject.GetComponentInParent<Sculpture>().SequenceComplete(this);

            // no parts left, so destroy this sequence (TODO: play rubble anim)
            Destroy(gameObject);
        }
    }

    SculptablePart GetNextPart()
    {
        if (parts.Count > 0)
        {
            part = parts[0];
            part.gameObject.SetActive(true);

            return part;
        }
    
        return null;
    }
}