using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxTutorial : MonoBehaviour
{
    public List<Sprite> anim = new List<Sprite>();


    public SpriteRenderer animDisplay;
    public int animInt;

    public float frameTime, originalFrameTime;

    void Start()
    {
        originalFrameTime = frameTime;

        UpdateSpriteDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        frameTime -= Time.deltaTime;

        if (frameTime <= 0)
        {
            IncrementFrame();
            UpdateSpriteDisplay();
        }
    }

    public void IncrementFrame()
    {
        if (CheckListLength(animInt, anim))
        {
            animInt++;
        }
        else
        {
            animInt = 0;
        }

        frameTime = originalFrameTime;
    }

    public bool CheckListLength(int count, List<Sprite> list)
    {
        return count < list.Count - 1;
    }

    public void UpdateSpriteDisplay()
    {
        animDisplay.sprite = anim[animInt];

    }

    //complicated function to get rid of the tutorial!!
    public void DeleteTutorial()
    {
        Destroy(this.gameObject);
    }
}
