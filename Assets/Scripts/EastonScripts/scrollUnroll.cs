using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class scrollUnroll : MonoBehaviour
{
    public float dropHeight, speed, startingHeight;
    public SpriteRenderer textImage;
    public delegate void Callback();

    private void EmptyCallback() { }

    private void Start()
    {
        TweenUp(true);
    }

    public void TweenDown(Sprite image)
    {
        Vector3 newPos = new Vector3(transform.position.x, startingHeight - dropHeight, transform.position.z);

        textImage.sprite = image;
        TweenToNewPosition(newPos, EmptyCallback);
    }

    public void TweenDown(Sprite image, Callback callback, float delayBeforeCallback = 0)
    {
        Vector3 newPos = new Vector3(transform.position.x, startingHeight - dropHeight, transform.position.z);

        textImage.sprite = image;
        TweenToNewPosition(newPos, callback, false, delayBeforeCallback);
    }

    public void TweenUp(bool snap = false)
    {
        Vector3 newPos = new Vector3(transform.position.x, startingHeight, transform.position.z);

        TweenToNewPosition(newPos, EmptyCallback, snap);
    }

    public void TweenUp(Callback callback, bool snap = false, float delayBeforeCallback = 0)
    {
        Vector3 newPos = new Vector3(transform.position.x, startingHeight, transform.position.z);

        TweenToNewPosition(newPos, callback, snap, delayBeforeCallback);
    }

    void TweenToNewPosition(Vector3 position, Callback callback, bool snap = false, float delayBeforeCallback = 0)
    {
        if (delayBeforeCallback == 0)
        {
            transform.DOMoveY(position.y, speed, snap).OnComplete(() => callback());
        }
        else
        {
            Sequence scrollSequence = DOTween.Sequence();
            scrollSequence.Append(transform.DOMoveY(position.y, speed, snap)).AppendInterval(delayBeforeCallback).AppendCallback(() => callback());
        }

    }
}
