using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float cursorSensitivity;
    public float requiredVelocity;
    public float maxRecoil = 30;
    public float recoilDuration = 0.3f;

    private Rigidbody2D hammerBody;
    private Vector3 previousVelocity;
    private float recoilTime = 0f;
    private float recoilForceX;
    private float recoilForceY;
    private float recoveryTime = 0f;

    private void Awake()
    {
        Cursor.visible = false;
        hammerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (recoilTime == 0)
        {
            previousVelocity = hammerBody.velocity;

            Vector3 mouseMovement = new Vector3(Input.GetAxisRaw("Mouse X") * cursorSensitivity, Input.GetAxisRaw("Mouse Y") * cursorSensitivity, 0f);
            hammerBody.velocity = mouseMovement;
        }
        else
        {
            recoilTime -= Time.deltaTime;

            if (recoilTime <= 0)
            {
                hammerBody.velocity = Vector2.zero;
                recoilTime = 0;
                recoveryTime = recoilDuration / 2;
            }   
            else
            {
                Quaternion rotation = Quaternion.Euler(0, 0, -recoilForceX);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, recoilDuration);
            }
        }

        if (recoveryTime > 0)
        {
            recoveryTime -= Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, recoilDuration / 2);
        }
    }

    public void CollidedWithChisel(Chisel chisel)
    {
        if (chisel && previousVelocity.magnitude >= requiredVelocity) // if it was moving at the right velocity
        {
            recoilForceX = Mathf.Clamp(-previousVelocity.x, -maxRecoil, maxRecoil);
            recoilForceY = Mathf.Clamp(-previousVelocity.y, -maxRecoil, maxRecoil);

            recoilTime = recoilDuration;
            hammerBody.velocity = new Vector2(recoilForceX,recoilForceY);
           
            chisel.OnHammerCollision(); 
        }
    }
}