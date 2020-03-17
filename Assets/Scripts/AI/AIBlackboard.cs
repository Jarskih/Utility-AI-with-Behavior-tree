using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace AI
{
    [Serializable]
    public class AIBlackboard
    {
        public AIBehavior enemyTarget => _enemyTarget;
        public AIBehavior friendlyTarget => _friendlyTarget;

        public Dictionary<Keys, bool> memory => _memory;
        private Dictionary<Keys, bool> _memory;

        private AIBehavior _owner;
       [SerializeField] private AIBehavior _enemyTarget;
       [SerializeField] private AIBehavior _friendlyTarget;

        public enum Keys
        {
            HasEnemyTarget,
            HasFriendlyTarget,
            HasStamina,
            HasMana,
            LowMorale,
            HasWeapon,
            IsHurt,
            InRange
        }
        
        public AIBlackboard(AIBehavior owner)
        {
            _owner = owner;
            _enemyTarget = null;
            _friendlyTarget = null;
            _memory = new Dictionary<Keys, bool>();
        }
        
        public void Init()
        {
        }

        public void Tick(Sense sense, Stats stats)
        {
            SetTargets(sense);
            CheckRange(stats);
            _memory[Keys.IsHurt] = stats.health < stats.maxHealth;
            _memory[Keys.HasEnemyTarget] = _enemyTarget != null;
            _memory[Keys.HasFriendlyTarget] = _friendlyTarget != null;
            _memory[Keys.HasStamina] = stats.stamina > 10;
            _memory[Keys.HasMana] = stats.mana > 10;
            _memory[Keys.LowMorale] = stats.morale <= 0;
            _memory[Keys.HasWeapon] = stats.hasWeapon;
        }

        private void SetTargets(Sense sense)
        {
            foreach (var unit in sense.units)
            {
                if (unit == _owner) continue;
                
                if (unit.playerIndex == _owner.playerIndex)
                {
                    _friendlyTarget = unit;
                }
                else
                {
                    _enemyTarget = unit;
                }
            }
        }

        private void CheckRange(Stats stats)
        {
            if (_enemyTarget && Vector3.Distance(_owner.transform.position, _enemyTarget.transform.position) <
                stats.attackRange)
            {
                _memory[Keys.InRange] = true;
                return;
            }
            _memory[Keys.InRange] = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_enemyTarget.GetPosition(), Vector3.one * 0.1f);
        }
    }
}