using AI;

public struct DamageEvent
{
    public int damage;
    public AIBehavior attacker;
    public AIBehavior defender;

    public DamageEvent(int damage, AIBehavior attacker, AIBehavior defender)
    {
        this.damage = damage;
        this.attacker = attacker;
        this.defender = defender;
    }
}