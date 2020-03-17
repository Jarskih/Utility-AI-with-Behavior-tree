    using System;
    using System.Collections.Generic;
    using UnityEngine;

    namespace AI
    {
        [Serializable]
        public class Sense
        {
            public List<AIBehavior> units => _units;
            private List<AIBehavior> _units = new List<AIBehavior>();
            private Transform _transform;
            private float _sensingDistance = 50;

            public void Init(Transform transform)
            {
                _transform = transform;
            }
            public void Tick()
            {
                DetectEnemies();
            }

            private void DetectEnemies()
            {
                // TODO use entity manager
                var enemies = GameObject.FindGameObjectsWithTag("Unit");
                foreach (var enemy in enemies)
                {
                    if (Vector3.Distance(_transform.position, enemy.transform.position) > _sensingDistance)
                    {
                        continue;
                    }
                    
                    var ai = enemy.GetComponent<AIBehavior>();
                    if (ai.IsActive())
                    {
                        AddVisibleUnit(ai);
                    }
                    else
                    {
                        RemoveVisibleUnit(ai);
                    }
                }
            }

            private void AddVisibleUnit(AIBehavior agent)
            {
                if (!_units.Contains(agent))
                {
                    _units.Add(agent);
                }
            }
        
            private void RemoveVisibleUnit(AIBehavior agent)
            {
                if (_units.Contains(agent))
                {
                    _units.Remove(agent);
                }
            }

            private void ClearUnits()
            {
                _units.Clear();
            }
        }
    }