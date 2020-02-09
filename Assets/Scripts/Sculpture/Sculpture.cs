using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : MonoBehaviour
{
    List<SculptureSequence> sequences = new List<SculptureSequence>();
    private SculptureSettings thisSculpture;

    // approval
    public int approval;

    // falling
    private float fallAmount;
    private bool falling;
    private float fallDistance;
    private float fallDestination;
    private float startTime;

    private Manager manager;

    public void Init(SculptureSettings ss, Manager man)
    {
        thisSculpture = ss;
        approval = thisSculpture.initialApproval;
        manager = man;
        InitSequences(transform);    
    }

    private void InitSequences (Transform parent)
    {
        // Go recursively through objects, adding the SculptureSequence scripts to all objects with children
        for (var i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);

            if (child.childCount > 0 && !child.gameObject.GetComponent<SpriteRenderer>()) // it's a parent which means it's a sequence
            {
                child.gameObject.AddComponent<SculptureSequence>();

                var sequence = child.gameObject.GetComponent<SculptureSequence>();
                sequence.SetupParts(sequences.Count);
                sequences.Add(sequence);

                InitSequences(child);
            }
        }

    }

    private void Update()
    {
        if (falling)
        {
            float t = (Time.time - startTime) / fallDistance;

            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, fallDestination), t);

            if (transform.position.y == fallDestination)
            {
                falling = false;
            }
        }
    }

  
    public void SequenceComplete (SculptureSequence seq)
    {
        sequences.Remove(seq);
        CheckSculptureComplete();
    }

    private void CheckSculptureComplete ()
    {
        if (transform.childCount == 0 || (transform.childCount == 1 && transform.GetChild(0).gameObject.name == "DestroyMe")) // sculpture has been destroyed, add rubble
        {
            GameObject rubble = Instantiate(new GameObject(), GameData.GlobalGameData.transform.position, GameData.GlobalGameData.transform.rotation, transform);
            rubble.AddComponent<SpriteRenderer>().sprite = thisSculpture.rubble;
        }

    }

    public void CompleteEvent(GameEvent gameEvent)
    {
        switch (gameEvent.eventType)
        {
            case GameEvent.eventTypes.fall:
                Fall(gameEvent.eventValue);
                break;
            case GameEvent.eventTypes.points:
                UpdateApproval(gameEvent.eventValue);
                break;
                

        }
    }

    private void UpdateApproval(int points)
    {
        approval += points;
    }

    private void Fall(float value)
    {
        fallDistance = value;
        fallDestination = transform.position.y - value;
        falling = true;
        startTime = Time.time;
        fallAmount = value;
    }
}
