using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptureSequence : MonoBehaviour
{

    private List<SculptablePart> parts = new List<SculptablePart>();
    private SculptablePart part;

    public void SetupParts(int sortingLayer)
    {
        InitParts(sortingLayer);
        GetNextPart();
    }

    private void InitParts(int sortingLayer)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var childObj = transform.GetChild(i).gameObject;
            var sprite = childObj.GetComponent<SpriteRenderer>();

            if (sprite) // it's a sprite, which means it's a statue part
            {
                childObj.SetActive(false); // all statue parts start off disabled
                sprite.sortingOrder = sortingLayer;

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
            // no parts left, so destroy this sequence (TODO: play rubble anim)
            Destroy(gameObject);
            gameObject.name = "DestroyMe";
            gameObject.GetComponentInParent<Sculpture>().SequenceComplete(this);
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