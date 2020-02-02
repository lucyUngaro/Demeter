using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    private int currentStatueNum = 0;
    private GameObject currentStatue;
    private List <GameObject> sculptures;

    private void Awake()
    {
        sculptures = SculptureData.GlobalSculptureData.sculptures;

        CreateNextStatue();
    }

    public void SendOutStatue()
    {
        Destroy(currentStatue);
        CreateNextStatue();
    }

    private void CreateNextStatue()
    {
        if (sculptures.Count > currentStatueNum)
        {
            currentStatue = Instantiate(sculptures[currentStatueNum]);

            if (!currentStatue.GetComponent<Sculpture>())
            {
                currentStatue.AddComponent<Sculpture>();
            }

            currentStatue.GetComponent<Sculpture>().Init(currentStatueNum, this);
            currentStatueNum++;
        }
    }
}
