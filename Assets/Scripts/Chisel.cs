using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chisel : MonoBehaviour
{
    public float chiselSpeed;

    [HideInInspector]
    public SculptablePart selectedPart; 

    void Update()
    {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * chiselSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * chiselSpeed;

        transform.Translate(x, 0, 0);
        transform.Translate(0, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        selectedPart = collision.GetComponent<SculptablePart>();
        Debug.Log(selectedPart);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        selectedPart = null;
        Debug.Log(selectedPart);


    }

    public void OnHammerCollision()
    {
        if (selectedPart != null)
        {
            selectedPart.OnHit();
        }
    }
  
}
