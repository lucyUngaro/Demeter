using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollUnroll : MonoBehaviour
{
    public bool rolled;
    public float dropHeight, speed, startingHeight;
    public int currentPrompt;
    public List<SpriteRenderer> promptImages = new List<SpriteRenderer>();
    public bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        rolled = true;
        currentPrompt = 0;
        startingHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos;
        //
        //if done, roll up the scroll

        //if just starting game, slight delay before first scroll, each scroll should be submitted by the end of day for tehe


        if(Input.GetKeyDown(KeyCode.Space)){
            rolled = !rolled;
            if (rolled)
            {
               // prompt++;

            }
        }

        if(rolled){
            newPos = new Vector3(transform.position.x, startingHeight, transform.position.z);
            for (int i = 0; i < promptImages.Count; i++)
            {
                //if(i)
            }
        }
        else
        {
            newPos = new Vector3(transform.position.x, startingHeight - dropHeight, transform.position.z);
            for (int i = 0; i < promptImages.Count; i++)
            {

            }
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
    }
}
