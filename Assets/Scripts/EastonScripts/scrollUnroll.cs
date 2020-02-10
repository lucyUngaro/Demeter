using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class scrollUnroll : MonoBehaviour
{
    public float dropHeight, speed, startingHeight;
    public SpriteRenderer textImage;

    private void Start()
    {
        TweenUp(true);
    }

    public void TweenDown(Sprite image)
    {
        Vector3 newPos = new Vector3(transform.position.x, startingHeight - dropHeight, transform.position.z);

        textImage.sprite = image;

        TweenToNewPosition(newPos);
    }

    public void TweenUp(bool snap = false)
    {
        Vector3 newPos = new Vector3(transform.position.x, startingHeight, transform.position.z);
        TweenToNewPosition(newPos, snap);
    }

    void TweenToNewPosition(Vector3 position, bool snap = false)
    {
        DOTween.Clear();

        transform.DOMoveY(position.y, speed, snap);

    }
}
