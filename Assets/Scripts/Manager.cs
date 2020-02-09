using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private int currentStatueNum = 0;
    private GameObject currentStatue;
    public scrollUnroll scroll;
    private List <SculptureSettings> sculptureSettings;

    private void Awake()
    {
        sculptureSettings = GameData.GlobalGameData.sculptureSettings;
        CreateNextStatue();
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name); 
        }
    }

    public void SendOutStatue()
    {
        int approval = currentStatue.GetComponent<Sculpture>().approval;
        Destroy(currentStatue);
        scroll.rolled = true;
        //roll scroll set to true
        //wait a second then unroll scroll and create next statue
        StartCoroutine("WaitToCreateNext"); 
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
            currentStatueNum++;
        }
    }

    IEnumerator WaitToCreateNext()
    {
       yield return new WaitForSeconds(1);

        CreateNextStatue();
    }
}
