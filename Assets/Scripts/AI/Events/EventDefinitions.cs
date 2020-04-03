using System;

namespace AI.Events
{
    [Serializable]
    public struct EventDistanceData
    {
        public AIEventType type;
        public float distance;
    }
    
    public delegate void OnEventDelegate(AIEventData aiEventData);

    public enum AIEventType
    {
        Hurt,
        Taunt,
    }
}