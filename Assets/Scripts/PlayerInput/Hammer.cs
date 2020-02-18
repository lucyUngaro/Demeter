using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class MouseInput
{
    public Vector2 velocity;
    public float time;

    public MouseInput(Vector2 v, float t)
    {
        velocity = v;
        time = t;
    }
}
public class Hammer : MonoBehaviour
{
    public float cursorSensitivity;
    public float maxVelocity = 40f;
    public float minVelocity = 5f;
    public float requiredVelocity;
    public float maxRecoil = 30;
    public float recoilDuration = 1f;
    public float rotationThreshold = 5f;
    public float acceleration = 1.5f;
    public Sprite hammerStrike;
    public Sprite idle;

    private Rigidbody2D hammerBody;
    private Vector2 currentVelocity;

    private float recoilTime = 0f;
    private float recoilForceX;
    private float recoilForceY;
    private float idleTime = 0;
    private float movementDuration;
    private int currentDirection = 0;

  
    public ParticleSystem rocks;


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
            currentVelocity = this.GetCurrentInput();

            if (currentVelocity.magnitude > 1f)
            {
                if (idleTime >= 0.5)
                {
                    movementDuration = 0;
                }

                idleTime = 0;
                hammerBody.velocity = GetClampedVelocity(currentVelocity + (currentVelocity * movementDuration));
            }
            else
            {
                idleTime += Time.deltaTime;
                hammerBody.velocity *= 0.9f;
            }

            movementDuration += Time.deltaTime * acceleration;

            if (Mathf.Abs(hammerBody.velocity.x) > rotationThreshold)
            {
                ChooseDirection(hammerBody.velocity.x);
            }
        }
        else
        {
            movementDuration = 0;
            recoilTime -= Time.deltaTime;
            if (recoilTime <= 0)
            {
                hammerBody.velocity = Vector2.zero;
                recoilTime = 0;
                GetComponent<SpriteRenderer>().sprite = idle;
            }  
        }

    }

    private Vector2 GetClampedVelocity(Vector2 unclampedVelocity)
    {
        float maxX, maxY, minX, minY, x, y;
        maxX = maxY = maxVelocity;
        minX = minY = minVelocity;
        x = unclampedVelocity.x;
        y = unclampedVelocity.y;

        if (x > y) // clamp the axis with more movement
        {
            if (unclampedVelocity.x < 0)
            {
                maxX = minVelocity * -1;
                minX = maxVelocity * -1;
            }

            x = Mathf.Clamp(unclampedVelocity.x, minX, maxX);
        }
        else
        {
            if (unclampedVelocity.y < 0)
            {
                maxY = minVelocity * -1;
                minY = maxVelocity * -1;
            }

            y = unclampedVelocity.y == 0 ? 0 : Mathf.Clamp(unclampedVelocity.y, minY, maxY);
        }
      
        return new Vector2(x, y);
    }

    private Vector2 GetCurrentInput()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X") * cursorSensitivity, Input.GetAxisRaw("Mouse Y") * cursorSensitivity);
    }

    private void ChooseDirection(float moveValue)
    {
        int rotationValue = moveValue < 0 ? 1 : -1;

        if (currentDirection != 0 && rotationValue != currentDirection)
        {
            // switched rotation, reset all movement
            movementDuration = 0f;
        }

        currentDirection = rotationValue;

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * rotationValue, transform.localScale.y, transform.localScale.z);
    }

    public void CollidedWithChisel(Chisel chisel)
    {
        if (chisel && hammerBody.velocity.magnitude >= requiredVelocity && movementDuration > 0.2f) // if it was moving at the right velocity
        {
            recoilForceX = Mathf.Clamp(-hammerBody.velocity.x, -maxRecoil, maxRecoil);
            recoilForceY = Mathf.Clamp(-hammerBody.velocity.y, -maxRecoil, maxRecoil);

            recoilTime = recoilDuration;
            hammerBody.velocity = new Vector2(recoilForceX,recoilForceY);
           
            chisel.OnHammerCollision();

            transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - recoilForceX), recoilDuration).OnComplete(() => transform.DORewind());

            GetComponent<SpriteRenderer>().sprite = hammerStrike;

            if(!rocks.isPlaying){
                rocks.Play();
            }

            GetComponent<AudioMan>().playRandomSound();
        }
    }
}