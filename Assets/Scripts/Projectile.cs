using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using AI;

public class Projectile : MonoBehaviour
{
    private int _damage;
    private AIBehavior _owner;

    private void OnCollisionEnter(Collision other)
    {
        var enemy = other.gameObject.GetComponent<IDamageable>();

        if (enemy == null) return;
        
        var damageEvent = new DamageEvent(_owner, _damage);
        enemy.Damaged(damageEvent);
        gameObject.SetActive(false);
    }
}