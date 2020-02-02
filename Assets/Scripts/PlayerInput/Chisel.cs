using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chisel : MonoBehaviour
{
    public float chiselSpeed;

    private SculptablePart selectedPart; 

    void Update()
    {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * chiselSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * chiselSpeed;

        transform.Translate(x, 0, 0);
        transform.Translate(0, y, 0);
    }

    public void SelectPart(SculptablePart sp)
    {
        if (selectedPart != sp)
        {
            selectedPart = sp;
        }
    }

    public void DeselectPart(SculptablePart sp)
    {
        if (sp == selectedPart)
        {
            SelectPart(null);
        }
    }

    public void OnHammerCollision()
    {
        if (selectedPart != null)
        {
            selectedPart.OnHit();
        }
    }
  
}
