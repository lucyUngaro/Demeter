using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxAnimation : MonoBehaviour
{
    public List<Sprite> boxSprites = new List<Sprite>();
    public List<Sprite> boxBackSprites = new List<Sprite>();
    public SpriteRenderer boxBack;
    public float holdTimer;
    public float waitTimer = 0;
    public float waitTimerDuration = 2;
    public bool confirmed, flying, sent, waiting;
    public Vector3 startPos, lerpPos;
    public Manager gameMan;

    // Start is called before the first frame update
    void Start()
    {
        confirmed = false;
        flying = false;
        startPos = transform.position;
        sent = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(holdTimer >= 2.75f && !confirmed){
            confirmed = true;
            GetComponent<SpriteRenderer>().sprite = boxSprites[5];
            boxBack.sprite = boxBackSprites[5];
            transform.position = lerpPos;
        }else if (!confirmed){
            GetComponent<SpriteRenderer>().sprite = boxSprites[(int)(holdTimer * 2)];
            boxBack.sprite = boxBackSprites[(int)(holdTimer * 2)];
            transform.position = Vector3.Lerp(startPos, lerpPos, holdTimer/2.5f);
        }

        if(confirmed){
            GetComponent<SpriteRenderer>().sprite = boxSprites[5];
            boxBack.sprite = boxBackSprites[5];
            FindObjectOfType<Manager>().SendOutStatue();

            holdTimer = 0;
            confirmed = false;
            waitTimer = waitTimerDuration;
            waiting = true;
        }

        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer<= 0)
            {
                waitTimer = 0;
                waiting = false;
            }
        }


    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Theyre coming");
        if (!waiting && gameMan.gameRunning)
        {
            holdTimer += Time.deltaTime/2f;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Theyre gone");
        holdTimer = 0;
    }

}
