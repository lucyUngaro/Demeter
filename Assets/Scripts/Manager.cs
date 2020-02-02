using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Hammer hammer;
    public Chisel chisel;
    public List<GameObject> sculptures;

    int currentStatueNum = 0;
    GameObject currentStatue;

    private void Awake()
    {
        CreateNextStatue();
    }

    void CreateNextStatue()
    {
        if (sculptures.Count > currentStatueNum)
        {
            currentStatue = Instantiate(sculptures[currentStatueNum]);

            if (!currentStatue.GetComponent<Sculpture>())
            {
                currentStatue.AddComponent<Sculpture>();
            }

            currentStatueNum++;
        }
    }
}
