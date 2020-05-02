using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialAnimation : MonoBehaviour
{
    public List<Sprite> chisel = new List<Sprite>();
    public List<Sprite> hammer = new List<Sprite>();
    public List<Sprite> hit = new List<Sprite>();

    public SpriteRenderer chiselDisplay, hammerDisplay, hitDisplay;
    public int chiselInt, hammerInt, hitInt;

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

        if(frameTime <=0){
            IncrementFrame();
            UpdateSpriteDisplay();
        }
    }

    public void IncrementFrame(){
        if(CheckListLength(chiselInt, chisel)){
            chiselInt++;
        }else{
            chiselInt = 0;
        }

        if(CheckListLength(hammerInt, hammer)){
            hammerInt++;
        }else{
            hammerInt = 0;
        }

        if (CheckListLength(hitInt, hit)){
            hitInt++;
        }else{
            hitInt = 0;
        }

        frameTime = originalFrameTime;
    }

    public bool CheckListLength(int count, List<Sprite> list){
        if(count >= list.Count-1){
            return false;
        }else{
            return true;
        }
    }

    public void UpdateSpriteDisplay(){
        chiselDisplay.sprite = chisel[chiselInt];
        hammerDisplay.sprite = hammer[hammerInt];
        hitDisplay.sprite = hit[hitInt];
    }

    //complicated function to get rid of the tutorial!!
    public void DeleteTutorial(){
        Destroy(this.gameObject);
    }
}
