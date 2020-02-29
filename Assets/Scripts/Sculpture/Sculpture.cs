using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class oversees all of the sequences of parts in this sculpture, handles special events that are triggered when certain parts are destroyed,
 * and keeps track of the approval rating for this sculpture.
 * 
 * It is added automatically whenever the Manager instantiates a new sculpture prefab. It then goes recursively through its children and adds the 
 * SculptureSequence script to all objects that also have children. 
 * 
 **/
public class Sculpture : MonoBehaviour
{
    List<SculptureSequence> sequences = new List<SculptureSequence>();
    private SculptureSettings thisSculpture;

    public int approval;

    // falling (triggered by an event)
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

            if (child.childCount > 0 && !child.gameObject.GetComponent<SpriteRenderer>()) // if it's a parent, it should be a sequence of sculpture parts
            {
                child.gameObject.AddComponent<SculptureSequence>();

                var sequence = child.gameObject.GetComponent<SculptureSequence>();
                sequence.SetupParts(sequences.Count);
                sequences.Add(sequence);

                InitSequences(child);
            }
        }

    }

    // Execute an event set to occur on the destruction of an object specified in GameData
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

    // Once a sequence in this sculpture has been destroyed, check to see if the entire sculpture is destroyed
    public void SequenceComplete(SculptureSequence seq)
    {
        sequences.Remove(seq);
        CheckSculptureComplete();
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

    private void CheckSculptureComplete ()
    {
        if (sequences.Count == 0) // sculpture has been destroyed, add rubble
        {
            manager.OnSculptureComplete();

            GameObject rubble = Instantiate(new GameObject(), GameData.GlobalGameData.transform.position, GameData.GlobalGameData.transform.rotation, transform);
            rubble.AddComponent<SpriteRenderer>().sprite = thisSculpture.rubble;
        }

    }

  
    // Approval points are gained or lost based on what parts are destroyed
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
