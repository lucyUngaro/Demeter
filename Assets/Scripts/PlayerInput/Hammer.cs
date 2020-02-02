using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Hammer : MonoBehaviour
{
    public float cursorSensitivity;
    public float requiredVelocity;
    public float maxRecoil = 30;
    public float recoilDuration = 0.3f;
    public float rotationThreshold = 5f;
    public Sprite hammerStrike;
    public Sprite idle;

    private Rigidbody2D hammerBody;
    private Vector3 previousVelocity;
    private Vector3 previousVelocity2;
    private float recoilTime = 0f;
    private float recoilForceX;
    private float recoilForceY;

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
            previousVelocity2 = previousVelocity;
            previousVelocity = hammerBody.velocity;

            Vector3 mouseMovement = new Vector3(Input.GetAxisRaw("Mouse X") * cursorSensitivity, Input.GetAxisRaw("Mouse Y") * cursorSensitivity, 0f);
            hammerBody.velocity = mouseMovement;

            if (Mathf.Abs(mouseMovement.x) > rotationThreshold)
            {
                ChooseDirection(mouseMovement.x);
            }
        }
       else
        {
            recoilTime -= Time.deltaTime;

            if (recoilTime <= 0)
            {
                hammerBody.velocity = Vector2.zero;
                recoilTime = 0;
                GetComponent<SpriteRenderer>().sprite = idle;
            }  
        }

    }

    private void ChooseDirection(float moveValue)
    {
        int rotationValue = moveValue < 0 ? 1 : -1;

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * rotationValue, transform.localScale.y, transform.localScale.z);
    }

    public void CollidedWithChisel(Chisel chisel)
    {
        if (chisel && previousVelocity.magnitude >= requiredVelocity && previousVelocity.magnitude >= requiredVelocity) // if it was moving at the right velocity
        {
            recoilForceX = Mathf.Clamp(-previousVelocity.x, -maxRecoil, maxRecoil);
            recoilForceY = Mathf.Clamp(-previousVelocity.y, -maxRecoil, maxRecoil);

            recoilTime = recoilDuration;
            hammerBody.velocity = new Vector2(recoilForceX,recoilForceY);
           
            chisel.OnHammerCollision();

            transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - recoilForceX), recoilDuration).OnComplete(() => transform.DORewind());

            GetComponent<SpriteRenderer>().sprite = hammerStrike;

            if(!rocks.isPlaying){
                rocks.Play();
            }
        }
    }
}