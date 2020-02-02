using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptablePart : MonoBehaviour
{
    private Vector2 prevPartPos;

    private void Start()
    {
        transform.position = GameData.GlobalGameData.transform.position;
        transform.rotation = GameData.GlobalGameData.transform.rotation;
        transform.localScale = GameData.GlobalGameData.transform.scale;
    }

    public void Awaken(Vector2 pos)
    {
        gameObject.SetActive(true);

        if (pos != Vector2.zero)
        {
            prevPartPos = pos;
            StartCoroutine("UnityWorkaround");
        }

        FindObjectOfType<lightController>().AddSprite(GetComponent<SpriteRenderer>());
       
    }

    public void OnHit()
    {
        // Play destroy animation
        Destroy(gameObject);
        GetComponentInParent<SculptureSequence>().OnPartDestroyed();
    }
    
    // Yet another workaround for a bug in Unity -- inactive objects don't follow their parents, and when applying a new position directly after setting the object
    // active, it immediately gets overridden. 
    IEnumerator UnityWorkaround()
    {
        yield return new WaitForFixedUpdate();

        transform.localPosition = prevPartPos;
    }
 

}
