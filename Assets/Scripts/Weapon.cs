using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponStats _weaponWeaponStats;
    private WeaponColliderHolder _colliderHolder;
    private ProjectileSpawn _projectileSpawn;
    public int damage => _weaponWeaponStats.damage;

    void Init(string statsPath)
    {
        _projectileSpawn = GetComponentInChildren<ProjectileSpawn>();
        _colliderHolder = GetComponentInChildren<WeaponColliderHolder>();
    }

    public void Attack()
    {
        if (_weaponWeaponStats.weaponType == WeaponStats.WeaponType.Ranged)
        {
            var projectile = Instantiate(_weaponWeaponStats.projectile, _projectileSpawn.transform.position, _projectileSpawn.transform.rotation);
        }
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

    public void DeactivateCollider()
    {
        _colliderHolder.DisableCollider();
    }
}
