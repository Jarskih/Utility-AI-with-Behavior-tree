using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [Serializable]
    public class AbilityEvent : UnityEvent<int>
    {
    }
    
    private Dictionary<string, AbilityEvent> _eventDictionary;

    private static EventManager _eventManager;

    private static EventManager Instance
    {
        get
        {
            if (_eventManager)
            {
                return _eventManager;
            }

            _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

            if (!_eventManager)
            {
                Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
            }
            else
            {
                if (_eventManager != null)
                {
                    _eventManager.Init();
                }
            }

            return _eventManager;
        }
    }

    private void Init()
    {
        if (_eventDictionary == null)
        {
            _eventDictionary = new Dictionary<string, AbilityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<int> listener)
    {
        if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new AbilityEvent();
            thisEvent.AddListener(listener);
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName,  UnityAction<int> listener)
    {
        if (_eventManager == null)
        {
            return;
        }

        if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, int value)
    {
        if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            Debug.Log("Event triggered:" + eventName);
            thisEvent.Invoke(value);
        }
    }
}
