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
    public float maxRecoil = 30;
    public float recoilDuration = 1f;
    public float acceleration = 1.5f;
    public float velocityRequiredToCollide = 1.1f;
    public float idleTimeout = 0.3f;
    public float minimumCollideDistance = 4f;

    public Sprite hammerStrike;
    public Sprite idle;
    public ParticleSystem rocks;

    private Rigidbody2D hammerBody;
    private Vector2 currentVelocity;
    private Vector2 prevVelocity;
    private Vector2 startPosition = Vector2.zero;
   
    private float requiredVelocity = 100f;
    private float minVelocity = 100f;
    private float recoilTime = 0f;
    private float recoilForceX;
    private float recoilForceY;
    private float idleTime = 0f;
    private float movementDuration = 0;
    private float momentum = 0f;
    private int currentDirection = 0;
  


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

            if (currentVelocity.magnitude > 0f)
            {
                if (movementDuration == 0 || (currentVelocity.magnitude > 0 && currentVelocity.magnitude < minVelocity))
                {
                    // Started moving again, so set the minimum velocity
                    minVelocity = currentVelocity.magnitude;
                    requiredVelocity = minVelocity * velocityRequiredToCollide;
                    startPosition = hammerBody.position;
                }

                idleTime = 0;
                CalculateMomentum();

                hammerBody.velocity = currentVelocity + currentVelocity * momentum;

                movementDuration += Time.deltaTime;

                if (idleTime == 0)
                {
                    ChooseDirection(currentVelocity.x);
                }

                prevVelocity = currentVelocity;
            }
            else
            {

                idleTime += Time.deltaTime;

                if (idleTime >= idleTimeout)
                {
                    movementDuration = 0;
                    hammerBody.velocity = Vector2.zero;
                }

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

        CalculateMomentum();

    }

    private void CalculateMomentum()
    {
        momentum = movementDuration * acceleration;
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

        if (movementDuration >= 0.1f)
        {

            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * rotationValue, transform.localScale.y, transform.localScale.z);
        }
       
    }

    private bool MovingFastEnough ()
    {      
        return currentVelocity.magnitude >= requiredVelocity || (idleTime < idleTimeout && currentVelocity == Vector2.zero && prevVelocity.magnitude >= requiredVelocity);
    }

    private bool StartedFarEnoughAway(Transform collidedObj)
    {
        return Vector2.Distance(collidedObj.position, startPosition) > 4;
    }

    public void CollidedWithChisel(Chisel chisel)
    {
        if (chisel && MovingFastEnough() && StartedFarEnoughAway(chisel.transform)) // if it was moving at the right velocity
        {
            recoilForceX = Mathf.Clamp(-hammerBody.velocity.x, -maxRecoil, maxRecoil);
            recoilForceY = Mathf.Clamp(-hammerBody.velocity.y, -maxRecoil, maxRecoil);

            recoilTime = recoilDuration;
            hammerBody.velocity = new Vector2(recoilForceX,recoilForceY);
           
            chisel.OnHammerCollision();

            transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - recoilForceX), recoilDuration).OnComplete(() => transform.DORewind());

            GetComponent<SpriteRenderer>().sprite = hammerStrike;

            rocks.Play();

            GetComponent<AudioMan>().playRandomSound();
        }
    
     
    }
}