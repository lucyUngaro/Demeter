using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private int currentStatueNum = 0;
    public scrollUnroll scroll;
    public tutorialAnimation tutorial;

    private GameObject currentStatue;
    private List <SculptureSettings> sculptureSettings;
    private int currentApproval;

    public bool gameRunning;

    private void Awake()
    {
        sculptureSettings = GameData.GlobalGameData.sculptureSettings;
        CreateNextStatue();

        gameRunning = false;
    }

    public void OnSculptureComplete()
    {
        if (currentStatueNum == 1) // just completed the title (make a better check for this)
        {
            CreateNextStatue();
            gameRunning = true;
        }
    }

    public void DestroyTutorial()
    {
        tutorial.DeleteTutorial();
    }

    public void SendOutStatue()
    {
        currentApproval = currentStatue.GetComponent<Sculpture>().approval;
        Destroy(currentStatue);

        scroll.TweenUp();

        //roll scroll set to true
        //wait a second then unroll scroll and create next statue
        StartCoroutine("WaitToCreateNext"); 
    }

    private void ShowClientFeedback()
    {
        Sprite feedbackSprite = currentApproval > 0 ? sculptureSettings[currentStatueNum].positiveResponse : sculptureSettings[currentStatueNum].negativeResponse;
        scroll.TweenDown(feedbackSprite);
    }

    public void CreateNextStatue()
    {
        if (sculptureSettings.Count > currentStatueNum)
        {
            currentStatue = Instantiate(sculptureSettings[currentStatueNum].sculpture);

            if (!currentStatue.GetComponent<Sculpture>())
            {
                currentStatue.AddComponent<Sculpture>();
            }

            currentStatue.GetComponent<Sculpture>().Init(sculptureSettings[currentStatueNum], this);
            scroll.TweenDown(sculptureSettings[currentStatueNum].prompt);
            currentStatueNum++;
        }
        else
        {
            gameRunning = false; // game over
        }
    }

    IEnumerator WaitForClientFeedback()
    {
        yield return new WaitForSeconds(scroll.speed);

        ShowClientFeedback();
    }

    IEnumerator WaitToCreateNext()
    {
       yield return new WaitForSeconds(scroll.speed);

        CreateNextStatue();
    }
}
