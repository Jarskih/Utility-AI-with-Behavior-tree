using System.Collections.Generic;
using UnityEngine;

namespace AI.Events
{
    public class AIEventSystem : MonoBehaviour
    {
        public AIStimData stimData;
        public event OnEventDelegate AIEvent;

        public void TriggerEvent(AIBehavior source, AIBehavior target, params AIEventType[] parameters)
        {
            foreach (AIEventType aiEventType in parameters)
            {
                float radius = stimData.GetRadius(aiEventType);
                
                if (radius > 0)
                {
                    AIEventData aiEventData = new AIEventData(source, target, aiEventType, radius);
                    AIEvent?.Invoke(aiEventData);
                }
            }
        }
    }
}