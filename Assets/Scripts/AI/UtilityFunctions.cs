using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AI;
using UnityEngine;

[Serializable]
public class UtilityFunctions
{
    [SerializeField] private AnimationCurve _healCurve;
    [SerializeField] private AnimationCurve _attackCurve;
    [SerializeField] private AnimationCurve _tauntCurve;
    [SerializeField] private AnimationCurve _retreatCurve;

    public Dictionary<Decisions, float> priorities => _priorities;
    private Dictionary<Decisions, float> _priorities = new Dictionary<Decisions, float>();
    public enum Decisions
    {
        ShouldAttack,
        ShouldHeal,
        ShouldRetreat,
        ShouldBlock,
        ShouldTaunt
    }

    private float _maxDistance = 10f;

    public void Init()
    {
    }
    
    public void Decide(Dictionary<AIBlackboard.Keys, float> stats, UnitStats unitStats)
    {
        UpdatePriorities(Decisions.ShouldHeal, stats, unitStats);
        UpdatePriorities(Decisions.ShouldAttack, stats, unitStats);
        UpdatePriorities(Decisions.ShouldBlock, stats, unitStats);
        UpdatePriorities(Decisions.ShouldRetreat, stats, unitStats);
        UpdatePriorities(Decisions.ShouldTaunt, stats, unitStats);
    }
    
    private float CalculateUtility(float currentStat, float minValue, float maxValue, AnimationCurve curve)
    {
        var normalized = (currentStat - minValue) / (maxValue - minValue);
        return curve.Evaluate(normalized);
    }
    
    
    // Calculate utility values for available actions. Designed for a party of two to keep things simple
    private void UpdatePriorities(Decisions id, Dictionary<AIBlackboard.Keys, float> stats, UnitStats unitStats)
    {
        float utility;
        switch (id)
        {
            // Attack when full health
            case Decisions.ShouldAttack:
                utility = CalculateUtility(stats[AIBlackboard.Keys.Health], 0, unitStats.maxHealth, _attackCurve);
                _priorities[Decisions.ShouldAttack] = utility;
                break;
            // Run when low health
            case Decisions.ShouldRetreat:
                utility = CalculateUtility(stats[AIBlackboard.Keys.EnemyDistance], 0, _maxDistance, _retreatCurve);
                _priorities[Decisions.ShouldRetreat] = utility;
                break;
            // Archer heals when in low health
            case Decisions.ShouldHeal:
                var friendHealth = stats[AIBlackboard.Keys.FriendHealth];
                var health = stats[AIBlackboard.Keys.Health];

                if (health > friendHealth)
                {
                    utility = CalculateUtility(friendHealth, 0, unitStats.maxHealth, _healCurve);
                }
                else
                {
                    utility = CalculateUtility(health, 0, unitStats.maxHealth, _healCurve);
                }
                _priorities[Decisions.ShouldHeal] = utility;
                break;
            // Paladin taunts when enemies near archer
            case Decisions.ShouldTaunt:
                utility = CalculateUtility(stats[AIBlackboard.Keys.EnemyDistanceToFriend], 0, _maxDistance, _tauntCurve);
                _priorities[Decisions.ShouldTaunt] = utility;
                break;
        }
    }
}
