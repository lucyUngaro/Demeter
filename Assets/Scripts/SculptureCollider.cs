using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The part of the chisel that can collide with the sculpture */
public class SculptureCollider : MonoBehaviour
{
    Chisel chisel;

    private void Awake()
    {
        chisel = gameObject.GetComponentInParent<Chisel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        chisel.SelectPart(collision.GetComponent<SculptablePart>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        chisel.SelectPart(null);
    }
}
