using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private int currentStatueNum = 0;
    public scrollUnroll scroll;
    public boxAnimation boxAnimation;

    public Tutorial tutorialManager;

    private GameObject currentStatue;
    private List <SculptureSettings> sculptureSettings;
    private int currentApproval;

    public bool gameRunning;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        sculptureSettings = GameData.GlobalGameData.sculptureSettings;
        CreateNextStatue();
        tutorialManager.StartTutorial("controls");

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

    public void StartBoxTutorial()
    {
        tutorialManager.StartTutorial("box");
    }

    public void CompleteControlsTutorial()
    {
        tutorialManager.CompleteControlsTutorial();
    }

    public void SendOutStatue()
    {
        currentApproval = currentStatue.GetComponent<Sculpture>().approval;
        currentStatue.GetComponent<Sculpture>().DestroySculpture();
        tutorialManager.CompleteBoxTutorial();

        scroll.TweenUp(ShowClientFeedback);
    }

    private void ShowClientFeedback()
    {
        SculptureSettings settings = sculptureSettings[currentStatueNum - 1];
        Sprite feedbackSprite = currentApproval > 0 ? settings.positiveResponse : settings.negativeResponse;
        scroll.TweenDown(feedbackSprite, FinishedClientFeedback, 3);
    }

    private void FinishedClientFeedback()
    {
        scroll.TweenUp(CreateNextStatue);
        boxAnimation.ResetValues();

    }

    public void CreateNextStatue()
    {
        if (currentStatueNum >= sculptureSettings.Count)
        {
            gameRunning = false; // game over
        }

        if (sculptureSettings.Count > currentStatueNum)
        {

            if (!sculptureSettings[currentStatueNum].sculpture.GetComponent<Sculpture>())
            {
                sculptureSettings[currentStatueNum].sculpture.AddComponent<Sculpture>();
            }

            currentStatue = Instantiate(sculptureSettings[currentStatueNum].sculpture);
            currentStatue.GetComponent<Sculpture>().Init(sculptureSettings[currentStatueNum], this, currentStatueNum);
            scroll.TweenDown(sculptureSettings[currentStatueNum].prompt);
            currentStatueNum++;
        }
   
    }
}
