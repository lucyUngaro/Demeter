using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendStatue : MonoBehaviour
{

    public float highlighted, resting, packing, speed;
    public bool mousedOver = false;
    public bool mouseClicked = false;
    public bool sending = false;
    public Sprite open, closed;
    public SpriteRenderer box; 
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(0, -12.5f, -0.5f);
        resting = 0.5f;
        highlighted = 0.75f;
        packing = 1;
        speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if(mousedOver){
            transform.localScale = Vector3.Lerp(transform.localScale, highlighted * Vector3.one, Time.deltaTime * speed);

            if(mouseClicked){
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, -4, -0.5f), Time.deltaTime * speed);
                transform.localScale = Vector3.Lerp(transform.localScale, packing * Vector3.one, Time.deltaTime * speed);
                //swap statue
                //complete prompt
                if(Vector3.Distance(transform.position, new Vector3(0, -4, -0.5f)) < 0.75f){
                    box.sprite = closed;
                    mouseClicked = false;
                    sending = true;
                }
            }
        }else{
            transform.localScale = Vector3.Lerp(transform.localScale, resting * Vector3.one, Time.deltaTime * speed);
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, -12.5f, -0.5f), Time.deltaTime * speed);
        }

        if(Input.GetKey(KeyCode.Space)){
            //hover over collider
            mousedOver = true;
        }else{
            mousedOver = false;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //if you click on collider
            mouseClicked = true;
        }

        if(sending){
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 20, -0.5f), Time.deltaTime * speed);
        }

        if (Vector3.Distance(transform.position, new Vector3(0, 20, -0.5f)) < 0.5f)
        {
            //reset
            sending = false;
        }

    }
}
