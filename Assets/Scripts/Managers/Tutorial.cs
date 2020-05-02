using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject controlsTutorial;
    public GameObject boxTutorial;

    private GameObject currentControlsTutorial = null;
    private GameObject currentBoxTutorial = null;


    public void StartTutorial(string tutorial)
    {
        switch (tutorial)
        {
            case "controls":
                if (currentControlsTutorial == null)
                {
                    currentControlsTutorial = Instantiate(controlsTutorial);
                }

                break;
            case "box":
                if (currentBoxTutorial == null)
                {
                    currentBoxTutorial = Instantiate(boxTutorial);
                }
                break;
        }
     }

    public void CompleteControlsTutorial()
    {
        if (currentControlsTutorial)
        {
            Destroy(currentControlsTutorial);
            currentControlsTutorial = null;
        }
    }

    public void CompleteBoxTutorial()
    {
        if (currentBoxTutorial)
        {
            Destroy(currentBoxTutorial);
            currentBoxTutorial = null;
        }
    }
}
