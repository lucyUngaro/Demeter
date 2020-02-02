using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The part of the chisel that can collide with the sculpture */
public class SculptureCollider : MonoBehaviour
{
    Chisel chisel;
    SculptablePart currentPart;

    private void Awake()
    {
        chisel = gameObject.GetComponentInParent<Chisel>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        currentPart = collision.GetComponent<SculptablePart>();
        StartCoroutine("TriggerDetection");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        chisel.DeselectPart(collision.GetComponent<SculptablePart>());
    }

    IEnumerator TriggerDetection() // Workaround for bug with trigger detection
    {
        SculptablePart previousPart = currentPart;

        yield return new WaitForEndOfFrame();

        if (currentPart && previousPart && previousPart != currentPart) // they're Z fighting
        {
            currentPart = previousPart.GetComponent<SpriteRenderer>().sortingOrder > currentPart.GetComponent<SpriteRenderer>().sortingOrder ? previousPart : currentPart;
        }

        chisel.SelectPart(currentPart);

    }
}
