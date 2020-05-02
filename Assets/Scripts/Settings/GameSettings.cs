using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GameEvent
{
    public enum eventTypes { points, fall, boxTutorial, controlsTutorialComplete };

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
public struct SculptureSettings
{
    public GameObject sculpture;
    public Sprite rubble;
    public int initialApproval;
    public Sprite prompt, positiveResponse, negativeResponse;
}

[System.Serializable]
public class GameData
{
    public TransformPart transform;
    public List<SculptureSettings> sculptureSettings;
    public List<GameEvent> events;

    public static GameData GlobalGameData = null;

    public GameData()
    {
        GlobalGameData = this;
    }
}

public class GameSettings : MonoBehaviour
{
    public GameData sculptureData = new GameData();
}
