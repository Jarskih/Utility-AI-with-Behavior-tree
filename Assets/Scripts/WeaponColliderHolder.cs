using System;
using AI;
using UnityEngine;

public delegate void OnDamageEvent(IEventSource source, IDamageable target);
public class WeaponColliderHolder : MonoBehaviour
{
    public event OnDamageEvent damageEvent;
    
    private BoxCollider _collider;
    private Weapon _weapon;
    private IEventSource _source;

    public void Init()
    {
        _source = GetComponentInParent<IEventSource>();
        _weapon = GetComponentInParent<Weapon>();
        _collider = GetComponent<BoxCollider>();
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>() as AIBehavior;

        if (_source != null)
        {
            if ((AIBehavior) _source == damageable) return;
            
            damageEvent?.Invoke(_source, damageable);  
        }
    }
}