using System;
using AI;
using UnityEngine;

    [Serializable]
    public class Stats
    {
        private EntityManager _entityManager;
        private AIBehavior _aiBehavior;
        
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

        public void Init(EntityManager entityManager, AIBehavior aiBehavior)
        {
            _entityManager = entityManager;
            _aiBehavior = aiBehavior;
        }
        
        public void Tick()
        {
            UpdateMorale();
            
            _stamina += (int)(Time.deltaTime * recoverySpeed);
            _stamina = Mathf.Clamp(_stamina, 0, _maxStamina);
            _mana += (int)(Time.deltaTime * recoverySpeed);
            _mana = Mathf.Clamp(_mana, 0, _maxMana);
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
        }

        public void ReduceMana(int value)
        {
            _mana -= value;
        }

        public void ReduceMorale(int value)
        {
            _morale -= value;
        }
    }