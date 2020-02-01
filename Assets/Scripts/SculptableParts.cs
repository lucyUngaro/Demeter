using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptableParts : MonoBehaviour
{

    private GameObject part;

    // Start is called before the first frame update
    void Start()
    {
        GetNextPart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && part != null)
        {
            DestroyImmediate(part);
            part = GetNextPart();
        }
    }

    GameObject GetNextPart()
    {
        if (transform.childCount > 0)
        {
            part = transform.GetChild(0).gameObject;
            part.SetActive(true);
            return part;
        }
    
        return null;
    }
}