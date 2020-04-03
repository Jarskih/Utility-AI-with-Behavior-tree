using System;
using AI;
using UnityEngine;

[Serializable]
public class Stats
{
    private AIBlackboard _blackboard;
    public float maxHealth => _unitStats.maxHealth;
    public UnitStats unitStats => _unitStats;
    private UnitStats _unitStats;
    public int Healing => _unitStats.healing;
    public int HealingCost => _unitStats.healingCost;
    public int TauntCost => _unitStats.tauntCost;

    public void Init(EntityManager entityManager, AIBehavior aiBehavior, AIBlackboard blackboard, UnitStats unitStats)
    {
        _blackboard = blackboard;
        _unitStats = unitStats;
        
        if(unitStats == null) { Debug.LogError("Assign unit stats");}

        _blackboard.UpdateBlackBoard(AIBlackboard.Keys.Health, unitStats.maxHealth);
        _blackboard.UpdateBlackBoard(AIBlackboard.Keys.Mana, unitStats.maxMana);
        _blackboard.UpdateBlackBoard(AIBlackboard.Keys.Range, unitStats.attackRange);
    }

    public void Tick()
    {
        var mana = _blackboard.GetStatValue(AIBlackboard.Keys.Mana) + Time.deltaTime * _unitStats.recoverySpeed;
        mana = Mathf.Clamp(mana, 0, _unitStats.maxMana);
        _blackboard.SetBlackboardValue(AIBlackboard.Keys.Mana, mana);
    }
}