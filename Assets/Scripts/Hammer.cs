using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float cursorSensitivity;
    public float requiredVelocity;

    Rigidbody2D hammerBody;
    Vector3 previousVelocity;

    private void Awake()
    {
        Cursor.visible = false;
        hammerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        previousVelocity = hammerBody.velocity;

        Vector3 mouseMovement = new Vector3(Input.GetAxisRaw("Mouse X") * cursorSensitivity, Input.GetAxisRaw("Mouse Y") * cursorSensitivity, 0f);
        hammerBody.velocity = mouseMovement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var chisel = collision.gameObject.GetComponent<Chisel>();

        if (chisel && previousVelocity.magnitude >= requiredVelocity) // collided with the chisel
        {
            chisel.OnHammerCollision(); 
        }
    }
}
