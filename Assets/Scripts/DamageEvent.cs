using AI;
using UnityEngine;

public struct DamageEvent
{
    public int damage;
    public AIBehavior attacker;

    public DamageEvent(AIBehavior source, int damage)
    {
        this.damage = damage;
        attacker = source;
    }
}