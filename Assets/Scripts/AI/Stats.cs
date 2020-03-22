using System;
using AI;
using UnityEngine;

[Serializable]
public class Stats
{
    private EntityManager _entityManager;
    private AIBehavior _aiBehavior;
    private AIBlackboard _blackboard;
    
    public float health => _health;
    public float maxHealth => _maxHealth;
    public float stamina => _stamina;
    public float mana =>  _mana;
    public float morale => _morale;
    public bool hasWeapon => _hasWeapon;
    public float attackRange => _attackRange;

    [SerializeField]  private float _health;
    private readonly float _maxHealth;
    [SerializeField]  private float _stamina;
    private readonly float _maxStamina;
    [SerializeField]  private float _mana;
    private readonly float _maxMana;
    [SerializeField]  private float _morale;
    private readonly float _maxMorale;
    [SerializeField]  private bool _hasWeapon;
    [SerializeField] private float _attackRange;
    
    private float recoverySpeed = 1;
    private float _closeDistance = 3;

    public Stats()
    {
        _maxHealth = 100;
        _maxStamina = 100;
        _maxMana = 100;
        _maxMorale = 100;
        _health = _maxHealth;
        _stamina = _maxStamina;
        _mana = _maxMana;
        _morale = _maxMorale;

        _hasWeapon = true;
        _attackRange = 30;
    }

    public void Init(EntityManager entityManager, AIBehavior aiBehavior, AIBlackboard blackboard)
    {
        _entityManager = entityManager;
        _aiBehavior = aiBehavior;
        _blackboard = blackboard;
    }
    
    public void Tick()
    {
        UpdateMorale();
        
        _stamina += (int)(Time.deltaTime * recoverySpeed);
        _stamina = Mathf.Clamp(_stamina, 0, _maxStamina);
        _mana += (int)(Time.deltaTime * recoverySpeed);
        _mana = Mathf.Clamp(_mana, 0, _maxMana);
        
        _blackboard.Update(AIBlackboard.Keys.Mana, _mana);
        _blackboard.Update(AIBlackboard.Keys.Stamina, _stamina);
        _blackboard.Update(AIBlackboard.Keys.Morale, _morale);
        _blackboard.Update(AIBlackboard.Keys.Range, _attackRange);

        _blackboard.Update(AIBlackboard.Keys.IsHurt, _health < _maxHealth);
        _blackboard.Update(AIBlackboard.Keys.HasFriendlyTarget, _attackRange);
        _blackboard.Update(AIBlackboard.Keys.HasStamina, _stamina > 10);
        _blackboard.Update(AIBlackboard.Keys.HasMana, _mana > 10);
        _blackboard.Update(AIBlackboard.Keys.HasWeapon, _hasWeapon);
        _blackboard.Update(AIBlackboard.Keys.IsDead, _health <= 0);
        _blackboard.Update(AIBlackboard.Keys.LowMorale, _morale < 50);
    }
    
    private void UpdateMorale()
    {
        var enemies = _entityManager.GetEnemyEntities(_aiBehavior.playerIndex);
        int numberOfEnemiesNear = 0;
        
        foreach (var enemy in enemies)
        {
            if ((enemy.transform.position - _aiBehavior.transform.position).sqrMagnitude <
                _closeDistance * _closeDistance)
            {
                numberOfEnemiesNear += 1;
            }
        }

        if (numberOfEnemiesNear > 0)
        {
            var moraleLost = Mathf.Log(numberOfEnemiesNear);
            _morale -= moraleLost;
        }
        else
        {
            _morale += Time.deltaTime * recoverySpeed;
        }
        
        _morale = Mathf.Clamp(_morale, 0, _maxMorale);
    }

    public void ReduceHealth(int value)
    {
        _health -= value;
        _blackboard.Update(AIBlackboard.Keys.Health, health);
    }

    public void ReduceMana(int value)
    {
        _mana -= value;
        _blackboard.Update(AIBlackboard.Keys.Mana, _mana);
    }

    public void ReduceMorale(int value)
    {
        _morale -= value;
        _blackboard.Update(AIBlackboard.Keys.Morale, _morale);
    }
}