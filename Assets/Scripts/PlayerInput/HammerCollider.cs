using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The part of the chisel that can collide with the hammer */

public class HammerCollider : MonoBehaviour
{
    Chisel chisel;

    private void Awake()
    {
        chisel = gameObject.GetComponentInParent<Chisel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hammer = collision.gameObject.GetComponent<Hammer>();

        if (hammer)
        {
            hammer.CollidedWithChisel(chisel);
        }
    }
}
