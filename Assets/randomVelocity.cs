using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomVelocity : MonoBehaviour
{
    public float speedOffset;
    public float lastYval, timer;
    public bool moved;

    // Start is called before the first frame update
    void Start()
    {
        speedOffset = Random.Range(-25f, 25f);
        moved = false;
        lastYval = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //check the distances
        timer += Time.deltaTime;

        if(timer >= 0.1f){
            lastYval = transform.position.y;
            timer = 0;
        }

        if(transform.position.y != lastYval){
            moved = true;
        }

        if(moved){
            transform.position -= new Vector3(speedOffset * Time.deltaTime, Mathf.Abs(speedOffset) * Time.deltaTime, 0f); 
        }

    }
}
