using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxAnimation : MonoBehaviour
{
    public List<Sprite> boxSprites = new List<Sprite>();
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
        if(Input.GetKey(KeyCode.Space) && !waiting && gameMan.gameRunning){
            holdTimer += Time.deltaTime;
        }else{
            holdTimer = 0;
        }

        if(holdTimer >=2 && !confirmed){
            confirmed = true;
            GetComponent<SpriteRenderer>().sprite = boxSprites[7];
            transform.position = lerpPos;
        }else if (!confirmed){
            GetComponent<SpriteRenderer>().sprite = boxSprites[(int)(holdTimer * 3)];
            transform.position = Vector3.Lerp(startPos, lerpPos, holdTimer);
        }

        if(confirmed){
            GetComponent<SpriteRenderer>().sprite = boxSprites[7];
            if(holdTimer>2.5f){
                flying = true;
            }

            FindObjectOfType<Manager>().SendOutStatue();

            holdTimer = 0;
            confirmed = false;
            waitTimer = waitTimerDuration;
            waiting = true;
        }

        if(flying){
            GetComponent<SpriteRenderer>().sprite = boxSprites[9];
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 30, -0.5f), Time.deltaTime * 3f);

            if(Vector3.Distance(transform.position, new Vector3(0, 30, -0.5f)) < 0.5f){
                sent = true;
            }
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
}
