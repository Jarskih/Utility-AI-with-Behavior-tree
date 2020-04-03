using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace AI
{
    [Serializable]
    public class AIBlackboard
    {
        public AIBehavior enemyTarget => _enemyTarget;
        public AIBehavior friendlyTarget => _friendlyTarget;
        
        private Dictionary<Keys, bool> _memory;
        private Dictionary<Keys, float> _stats;
        
        private UtilityFunctions _utility;
        private AIBehavior _owner;
        
        private AIBehavior _enemyTarget;
        private AIBehavior _friendlyTarget;

       private float _maxDist = 10;

       public enum Keys
        {
            HasEnemyTarget,
            HasFriendlyTarget,
            LowMorale,
            IsHurt,
            InRange,
            Health,
            Mana,
            IsDead,
            Range,
            FriendHealth,
            EnemyDistance,
            EnemyDistanceToFriend
        }

       public AIBlackboard(AIBehavior owner, UtilityFunctions utility)
        {
            _owner = owner;
            _utility = utility;
            _enemyTarget = null;
            _friendlyTarget = null;
            _memory = new Dictionary<Keys, bool>();
            _stats = new Dictionary<Keys, float>();
        }
        
        public void Init()
        {
        }
        public void Tick(Sense sense)
        {
            // Update blackboard
            SetTargets(sense);
            QueryFriendsForTargets(sense);
            
            if (_friendlyTarget)
            {
                UpdateBlackBoard(Keys.FriendHealth, _friendlyTarget.blackboard.GetStatValue(Keys.Health));
            }
            else
            {
                UpdateBlackBoard(Keys.FriendHealth, 100);
            }
            
            UpdateBlackBoard(Keys.InRange, CheckRange());
            UpdateBlackBoard(Keys.HasEnemyTarget,  _enemyTarget != null);
            UpdateBlackBoard(Keys.HasFriendlyTarget, _friendlyTarget != null);
            UpdateBlackBoard(Keys.IsDead, GetStatValue(Keys.Health) <= 0);
            SetBlackboardValue(Keys.EnemyDistance, CalculateEnemyDist());
            SetBlackboardValue(Keys.EnemyDistanceToFriend, CalculateEnemyDistToFriend());

            // Decide
            _utility.Decide(_stats, _owner.stats.unitStats);
        }
        private float CalculateEnemyDistToFriend()
        {
            if (friendlyTarget == null || _owner.blackboard._enemyTarget == null) return _maxDist; // Use max distance (10) if no enemy target or friendly target

            var dist = Vector3.Distance(friendlyTarget.transform.position , _owner.blackboard._enemyTarget.transform.position);
            Mathf.Clamp(dist, 0, _maxDist);
            return dist;
        }

        private float CalculateEnemyDist()
        {
            if (enemyTarget == null) return _maxDist; // Use max distance if no enemy target

            var dist = Vector3.Distance(enemyTarget.transform.position , _owner.transform.position);
            Mathf.Clamp(dist, 0, _maxDist);
            return dist;
        }

        public bool GetMemoryValue(Keys key)
        {
            if (_memory.ContainsKey(key))
            {
                return _memory[key];
            }

            return false;
        }
        
        public float GetStatValue(Keys key)
        {
            if (_stats.ContainsKey(key))
            {
                return _stats[key];
            }
            return 0;
        }

        // Overwrite value
        public void SetBlackboardValue(Keys key, float value)
        {
            _stats[key] = value;
        }

        // Add to existing value
        public void UpdateBlackBoard(Keys key, float value)
        {
            if (_stats.ContainsKey(key))
            {
                _stats[key] += value;
            }
            else
            {
                _stats[key] = value;
            }
        }

        public void UpdateBlackBoard(Keys key, bool value)
        {
            _memory[key] = value;
        }

        public void SetEnemyTarget(AIBehavior owner)
        {
            _enemyTarget = owner;
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
                    if (!unit.IsActive()) continue;
                    
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
                else
                {
                    if (_enemyTarget != null) continue;

                    if (!unit.IsActive()) continue;
                    
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
        
        // Share information about enemies with friendly units
        private void QueryFriendsForTargets(Sense sense)
        {
            if (enemyTarget != null) return;

            foreach (var unit in sense.units)
            {
                if (unit.playerIndex == _owner.playerIndex)
                {
                    if (!unit.IsActive()) continue;
                    if (unit.blackboard._enemyTarget == null) continue;
                    if (unit.blackboard._enemyTarget.IsActive() == false) continue;

                    if ((unit.transform.position - _owner.transform.position).sqrMagnitude <
                        sense.sensingDistance * sense.sensingDistance)
                    {
                        _enemyTarget = unit.blackboard._enemyTarget;
                        break;    
                    }
                }
            }
        }


        // Determine if unit is within vision range
        private bool CheckRange()
        {
            var range = GetStatValue(Keys.Range);
            if (_enemyTarget && (_owner.transform.position - _enemyTarget.transform.position).sqrMagnitude <
                range*range)
            {
                return true;
            }

            return false;
        }
    }
}