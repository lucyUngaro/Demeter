using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SculptureData
{
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;

    public static SculptureData GlobalSculptureData;

    public SculptureData()
    {
        GlobalSculptureData = this;
    }
}


public class Manager : MonoBehaviour
{
    public SculptureData sculptureData = new SculptureData(); // set the transform values of sculpture parts in inspector and then assign to a static variable

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
