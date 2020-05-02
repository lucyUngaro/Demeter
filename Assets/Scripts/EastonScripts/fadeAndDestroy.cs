using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeAndDestroy : MonoBehaviour
{
    public SpriteRenderer title;
    public bool clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        title = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            clicked = true;
        }

        if(clicked){
            title.color = Color.Lerp(title.color, new Color(1, 1, 1, 0), Time.deltaTime * 2f);
        }

        if(title.color.a <= 0.05f){
            Destroy(this.gameObject);
        }
    }
}
