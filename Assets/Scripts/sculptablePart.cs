using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sculptablePart : MonoBehaviour
{

    public List<GameObject> parts = new List<GameObject>();
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        for (int i = 0; i < transform.childCount; i++){
            parts.Add(transform.GetChild(i).gameObject);
            parts[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Destroy(parts[state]);
            state++;
        }

        parts[state].SetActive(true);

    }
}