using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    [Serializable]
    public class AIBlackboard
    {
        public AIBehavior enemyTarget => _enemyTarget;
        public AIBehavior friendlyTarget => _friendlyTarget;

        public Dictionary<Keys, bool> memory => _memory;
        private Dictionary<Keys, bool> _memory;

        public Dictionary<Keys, float> stats => _stats;
        public Transform waypoint { get; set; }

        private Dictionary<Keys, float> _stats;

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
            InRange,
            Health,
            Stamina,
            Mana,
            Morale,
            IsDead,
            Range
        }
        
        public AIBlackboard(AIBehavior owner)
        {
            _owner = owner;
            _enemyTarget = null;
            _friendlyTarget = null;
            _memory = new Dictionary<Keys, bool>();
            _stats = new Dictionary<Keys, float>();
        }
        
        public void Init()
        {
        }

        public void Update(Keys key, float value)
        {
            _stats[key] = value;
        }

        public void Update(Keys key, bool value)
        {
            _memory[key] = value;
        }
        public void Tick(Sense sense)
        {
            SetTargets(sense);
            CheckRange();
            
            Update(AIBlackboard.Keys.HasEnemyTarget,  _enemyTarget != null);
            Update(Keys.HasFriendlyTarget, _friendlyTarget != null);

        }

        private void SetTargets(Sense sense)
        {
            var ownerPos = _owner.transform.position;

            if (_enemyTarget?.IsActive() == false)
            {
                _enemyTarget = null;
            }

            if (_friendlyTarget?.IsActive() == false)
            {
                _friendlyTarget = null;
            }
            
            foreach (var unit in sense.units)
            {
                if (unit == _owner) continue;
                
                if (unit.playerIndex == _owner.playerIndex)
                {
                    if (_friendlyTarget != null) continue;
                    
                    if (unit.IsActive())
                    {
                        if (_friendlyTarget == null)
                        {
                            _friendlyTarget = unit;
                            continue;
                        }

                        // Enemy focuses always on closest target
                        var distToNewUnit = unit.transform.position - ownerPos;
                        var distToExistingTarget = _friendlyTarget.transform.position - ownerPos;
                        if (distToNewUnit.sqrMagnitude < distToExistingTarget.sqrMagnitude)
                        {
                            _friendlyTarget = unit;
                        }
                    }
                }
                else
                {
                    if (_enemyTarget != null) continue;
                    
                    if (unit.IsActive())
                    {
                        if (_enemyTarget == null)
                        {
                            _enemyTarget = unit;
                            continue;
                        }

                        // Enemy focuses always on closest target
                        var distToNewUnit = unit.transform.position - ownerPos;
                        var distToExistingTarget = _enemyTarget.transform.position - ownerPos;
                        if (distToNewUnit.sqrMagnitude < distToExistingTarget.sqrMagnitude)
                        {
                            _enemyTarget = unit;
                        }
                    }
                }
            }
        }

        private void CheckRange()
        {
            if (_enemyTarget && (_owner.transform.position - _enemyTarget.transform.position).sqrMagnitude <
                _stats[Keys.Range]*_stats[Keys.Range])
            {
                _memory[Keys.InRange] = true;
                return;
            }
            _memory[Keys.InRange] = false;
        }
    }
}