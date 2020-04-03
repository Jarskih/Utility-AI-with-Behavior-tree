using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponStats _weaponWeaponStats;
    private WeaponColliderHolder _colliderHolder;
    private ProjectileSpawn _projectileSpawn;
    private AIBehavior _aiBehavior;
    private int damage => _weaponWeaponStats.damage;

    public void Init()
    {
        _aiBehavior = GetComponent<AIBehavior>();
        _projectileSpawn = GetComponentInChildren<ProjectileSpawn>();
        _colliderHolder = GetComponentInChildren<WeaponColliderHolder>();
        if (_colliderHolder)
        {
            _colliderHolder.Init();
            _colliderHolder.damageEvent += OnHit;
        }
    }

    private void OnHit(IEventSource source, IDamageable target)
    {
        var damageEvent = new DamageEvent((AIBehavior)source, damage);
        target?.Damaged(damageEvent);
    }

    public void RangedAttack()
    {
        if (_weaponWeaponStats.weaponType != WeaponStats.WeaponType.Ranged) return;
        
        var t = _projectileSpawn.transform;
        var projectile = Instantiate(_weaponWeaponStats.projectile, t.position, t.rotation);
        projectile.GetComponent<Projectile>().Init(_weaponWeaponStats, _aiBehavior);
        projectile.GetComponent<Rigidbody>().velocity = _weaponWeaponStats.projectileVelocity * projectile.transform.forward;
    }
    public void Drop()
    {
    }

    public void PickUp()
    {
    }

    public void ActivateCollider()
    {
        _colliderHolder.EnableCollider();
    }
}
