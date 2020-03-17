using System;
using UnityEngine;

public class WeaponColliderHolder : MonoBehaviour
{
    private BoxCollider _collider;
    private Weapon _weapon;
    private IDamageable _owner;
    private void Start()
    {
        _owner = GetComponentInParent<IDamageable>();
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
        var damageable = other.GetComponent<IDamageable>();
        if (damageable == _owner) return;
        
        damageable?.Damaged(_weapon.damage);
    }
}