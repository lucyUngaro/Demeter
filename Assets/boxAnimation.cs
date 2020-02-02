using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxAnimation : MonoBehaviour
{
    public List<Sprite> boxSprites = new List<Sprite>();
    public float holdTimer;
    public bool confirmed, flying, sent;
    public Vector3 startPos, lerpPos;

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
        if(Input.GetKey(KeyCode.Space)){
            holdTimer += Time.deltaTime;
        }else{
            holdTimer = 0;
        }

        if(holdTimer >=3 && !confirmed){
            confirmed = true;
            GetComponent<SpriteRenderer>().sprite = boxSprites[7];
            transform.position = lerpPos;
        }else if (!confirmed){
            GetComponent<SpriteRenderer>().sprite = boxSprites[(int)(holdTimer * 3)];
            transform.position = Vector3.Lerp(startPos, lerpPos, holdTimer);
        }

        if(confirmed){
            GetComponent<SpriteRenderer>().sprite = boxSprites[7];
            if(holdTimer>3.5f){
                flying = true;
            }
        }

        if(flying){
            GetComponent<SpriteRenderer>().sprite = boxSprites[9];
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 30, -0.5f), Time.deltaTime * 3f);

            if(Vector3.Distance(transform.position, new Vector3(0, 30, -0.5f)) < 0.5f){
                sent = true;
            }
        }

        Debug.Log(holdTimer * 3);




    }
}
