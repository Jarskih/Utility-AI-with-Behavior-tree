using System;
using AI;
using UnityEngine;

public class WeaponColliderHolder : MonoBehaviour
{
    private BoxCollider _collider;
    private Weapon _weapon;
    private AIBehavior _owner;
    private void Start()
    {
        _owner = GetComponentInParent<AIBehavior>();
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
        if (damageable?.playerIndex == _owner.playerIndex) return;

        var damageEvent = new DamageEvent(_weapon.damage, damageable, _owner);
        damageable?.Damaged(damageEvent);
    }
}