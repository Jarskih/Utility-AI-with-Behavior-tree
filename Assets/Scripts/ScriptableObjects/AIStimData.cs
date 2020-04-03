using System;
using System.Collections.Generic;
using AI.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "AIStimData", fileName = "AIStimData")]
public class AIStimData : ScriptableObject
{
    [SerializeField] private List<EventDistanceData> _distanceData;

    public float GetRadius(AIEventType type)
    {
        var data = _distanceData.Find(x => x.type == type);
        return data.distance;
    }
}
