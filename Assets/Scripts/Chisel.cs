using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chisel : MonoBehaviour
{
    public float chiselSpeed;
    Collider2D selectedPart; 

    void Update()
    {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * chiselSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * chiselSpeed;

        transform.Translate(x, 0, 0);
        transform.Translate(0, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        selectedPart = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        selectedPart = null;
    }
}
