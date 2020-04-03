using System;
using AI;
using UnityEngine;

public delegate void OnDamageEvent(IEventSource source, IDamageable target);
public class WeaponColliderHolder : MonoBehaviour
{
    public event OnDamageEvent damageEvent;
    
    private BoxCollider _collider;
    private IEventSource _source;

    public void Init()
    {
        _source = GetComponentInParent<IEventSource>();
        _collider = GetComponent<BoxCollider>();
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>() as AIBehavior;

        if (_source != null)
        {
            if ((AIBehavior) _source == damageable) return;
            
            damageEvent?.Invoke(_source, damageable);
            _collider.enabled = false;
        }
    }
}