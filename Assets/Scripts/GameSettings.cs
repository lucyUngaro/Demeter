using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GameEvent
{
    public enum eventTypes { negativePoints, positivePoints, fall };

    public eventTypes eventType;
    public int eventValue;
    public Sprite sprite;
}

[System.Serializable]
public struct TransformPart
{
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;
}

[System.Serializable]
public class SculptureData
{
    public TransformPart transform;
    public List<GameObject> sculptures;
    public List<Sprite> rubble;
    public List<GameEvent> events;

    public static SculptureData GlobalSculptureData = null;

    public SculptureData()
    {
        GlobalSculptureData = this;
    }
}

public class GameSettings : MonoBehaviour
{
    public SculptureData sculptureData = new SculptureData();
}
