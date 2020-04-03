using UnityEngine;

namespace AI.Events
{
    public class AIEventData
    {
        public readonly AIBehavior owner;
        public readonly AIBehavior target;
        public readonly Vector3 ownerPos;
        public readonly AIEventType type;
        public readonly float radius;
        public readonly float value;
        
        public AIEventData(AIBehavior owner, AIBehavior target,  AIEventType type, float radius = 0, float value = 0)
        {
            this.owner = owner;
            this.target = target;
            ownerPos = owner.GetPosition();
            this.type = type;
            this.radius = radius;
            this.value = value;
        }
    }
}