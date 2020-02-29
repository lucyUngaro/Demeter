using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  A SculptablePart is one part (or image) in a SculptureSequence. When the hammer and chisel collide with it, it is destroyed, and the next part in the sequence is activated.
 * */

public class SculptablePart : MonoBehaviour
{
    private Vector2 prevPartPos;

    private bool isDirty = false;

    private void Start()
    {
        transform.position = GameData.GlobalGameData.transform.position;
        transform.rotation = GameData.GlobalGameData.transform.rotation;
        transform.localScale = GameData.GlobalGameData.transform.scale;
    }

    public void Awaken(Vector2 pos)
    {
        gameObject.SetActive(true);

        // It is possible that the sculpture moved while this part was inactive. Children don't follow their parents while they are inactive.
        // As a workaround, pass in the position of the previous part in this sequence, and on FixedUpdate(), move to that position. 
        if (pos != Vector2.zero)
        {
            prevPartPos = pos;
            isDirty = true;
        }

        FindObjectOfType<lightController>().AddSprite(GetComponent<SpriteRenderer>());
       
    }

    public void OnHit()
    {
        // Play destroy animation
        FindObjectOfType<lightController>().RemoveSprite(GetComponent<SpriteRenderer>());

        Destroy(gameObject);
        GetComponentInParent<SculptureSequence>().OnPartDestroyed();
    }

    // Wait for FixedUpdate() to set the position of this part. If done immediately, it will get overridden by MonoBehaviour applying a different position.
    private void FixedUpdate()
    {
        if (isDirty)
        {
            transform.localPosition = prevPartPos;
            isDirty = false;
        }
    }
 

}
