using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class boxAnimation : MonoBehaviour
{
    
    public List<Sprite> boxSprites = new List<Sprite>();
    public List<Sprite> boxBackSprites = new List<Sprite>();
    public SpriteRenderer boxBack;
    public float holdDuration = 1;
    public float waitTimer = 0;
    public float waitTimerDuration = 2;

    private float holdTimer;
    private bool confirmed, waiting, tweening = false;
    public Vector3 startPos, lerpPos;
    public Manager gameMan;
    Vector3 original;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        confirmed = false;
        startPos = transform.position;
        waiting = false;
        original = transform.localScale;
        holdTimer = holdDuration;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(holdTimer <= 0f && !confirmed)
        {
            confirmed = true;
            transform.position = lerpPos;
        }
        else if (!confirmed)
        {
            float spriteCounter = Mathf.Lerp(0, 5.75f, 1 - holdTimer/3);
            GetComponent<SpriteRenderer>().sprite = boxSprites[(int)spriteCounter];
            boxBack.sprite = boxBackSprites[(int)spriteCounter];
            transform.position = Vector3.Lerp(startPos, lerpPos, 1 - holdTimer / holdDuration); 
        }

        if(confirmed && !tweening)
        {
            GetComponent<SpriteRenderer>().sprite = boxSprites[5];
            boxBack.sprite = boxBackSprites[5];

            gameMan.SendOutStatue();

            tweening = true;
            transform.DOMoveY(30, 1);
            
        }

        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                waitTimer = 0;
                waiting = false;
            }
        }

        if (!waiting && gameMan.gameRunning && Input.GetMouseButton(0))
        {
            holdTimer -= Time.deltaTime;
        }
        else
        {
            holdTimer = holdDuration;
        }


    }

    public void ResetValues()
    {
        transform.position = startPos;
        confirmed = false;
        waitTimer = waitTimerDuration;
        waiting = true;
        holdTimer = holdDuration;
        tweening = false;
    }

}
