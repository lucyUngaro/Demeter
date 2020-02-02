using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollUnroll : MonoBehaviour
{
    public bool rolled;
    public float dropHeight, speed, startingHeight;
    public int currentPrompt;
    public List<SpriteRenderer> promptImages = new List<SpriteRenderer>();
    public bool promptChanged;
    public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        rolled = true;
        promptChanged = false;
        currentPrompt = 0;
        startingHeight = transform.position.y;
        for (int i = 0; i < transform.childCount; i++){
            promptImages.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos;


        if(Input.GetKeyDown(KeyCode.Space)){
            rolled = !rolled;

        }

        if(rolled){
            newPos = new Vector3(transform.position.x, startingHeight, transform.position.z);
            if(!promptChanged && (Vector3.Distance(newPos, transform.position) < 0.1)){
                
                for (int i = 0; i < promptImages.Count; i++)
                {
                    if(i == currentPrompt){
                        promptImages[i].color = new Color(promptImages[i].color.r, promptImages[i].color.g, promptImages[i].color.b, 1);
                    }else{
                        promptImages[i].color = new Color(promptImages[i].color.r, promptImages[i].color.g, promptImages[i].color.b, 0);
                    }
                }
                currentPrompt++;
                promptChanged = true;
            }
        }
        else
        {
            newPos = new Vector3(transform.position.x, startingHeight - dropHeight, transform.position.z);
            promptChanged = false;
        }

        if (promptChanged){
            rolled = false;
            manager.CreateNextStatue();
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
    }
}
