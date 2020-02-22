using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxAnimation : MonoBehaviour
{
    
    public List<Sprite> boxSprites = new List<Sprite>();
    public List<Sprite> boxBackSprites = new List<Sprite>();
    public SpriteRenderer boxBack;
    public float holdTimer = 3;
    public float waitTimer = 0;
    public float waitTimerDuration = 2;

    public bool confirmed, flying, waiting;
    public Vector3 startPos, lerpPos;
    public Manager gameMan;
    Vector3 original;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        confirmed = false;
        flying = false;
        startPos = transform.position;
        waiting = false;
        original = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(holdTimer <= 0f && !confirmed){
            confirmed = true;
            transform.position = lerpPos;
        }else if (!confirmed){
            float spriteCounter = Mathf.Lerp(0, 5.75f, 1 - holdTimer/3);
            GetComponent<SpriteRenderer>().sprite = boxSprites[(int)spriteCounter];
            boxBack.sprite = boxBackSprites[(int)spriteCounter];
            transform.position = Vector3.Lerp(startPos, lerpPos, 1 - holdTimer/3f);
        }

        if(confirmed){
            GetComponent<SpriteRenderer>().sprite = boxSprites[5];
            boxBack.sprite = boxBackSprites[5];
            //FindObjectOfType<Manager>().SendOutStatue();
            gameMan.SendOutStatue();
            //GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log("sent statue");
            confirmed = false;
            waitTimer = waitTimerDuration;
            waiting = true;
            holdTimer = 3;
        }else{
           // GetComponent<BoxCollider2D>().enabled = true;
        }

        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            //Debug.Log("waiting");
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
            holdTimer = 3;
        }


    }
    /*
    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(holdTimer);
        if (!waiting && gameMan.gameRunning)
        {
            
            Vector3 bigger = original * 1.1f;
            transform.localScale = Vector3.Lerp(transform.localScale, bigger, Time.deltaTime*3f);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Theyre gone");
        //holdTimer = 0;
        transform.localScale = original;
    }*/

}
