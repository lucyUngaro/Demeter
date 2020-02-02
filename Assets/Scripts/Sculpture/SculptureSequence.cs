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

        // is there an event for the destruction of this sprite?
        CheckForEvent();
        Destroy(part.gameObject);

        part = GetNextPart(part.transform.localPosition);
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

    private void CheckForEvent()
    {
        List<GameEvent> allEvents = GameData.GlobalGameData.events;

        for (var i = 0; i < allEvents.Count; i++)
        {
            GameEvent gameEvent = allEvents[i];
            if (gameEvent.sprite == part.GetComponent<SpriteRenderer>().sprite)
            {
                gameObject.GetComponentInParent<Sculpture>().CompleteEvent(gameEvent);
            }
        }
    }

    private SculptablePart GetNextPart(Vector2 pos = default)
    {
        if (parts.Count > 0)
        {
            part = parts[0];
            part.Awaken(pos);

            return part;
        }
    
        return null;
    }
}