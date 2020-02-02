using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chisel : MonoBehaviour
{
    public float chiselSpeed;
    public float handShake = 0.6f;

    private SculptablePart selectedPart; 

    private float moveSpeed = 0f;
    private float timer = 0f;
    private float xLerp = 0f;
    private float yLerp = 0f;


    void Update()
    {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * chiselSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * chiselSpeed;

        transform.Translate(x, 0, 0);
        transform.Translate(0, y, 0);

        if (x == 0 && y == 0 && timer <= 0)
        {
            moveSpeed = Random.Range(0.3f, 1f);
            timer = Random.Range(0.3f, 0.8f);

            xLerp = transform.position.x + Random.Range(-handShake, handShake);
            yLerp = transform.position.y + Random.Range(-handShake, handShake);
        }

        if (timer > 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(xLerp, yLerp, 0f), moveSpeed * Time.deltaTime);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
            }
        }
        

       
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
