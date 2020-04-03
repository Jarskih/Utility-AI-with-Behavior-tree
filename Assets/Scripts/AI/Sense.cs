    using System;
    using System.Collections.Generic;
    using UnityEngine;

    namespace AI
    {
        [Serializable]
        public class Sense
        {
            // List of all detected entities
            public List<AIBehavior> units => _units;
            public float sensingDistance => _unitStats.sensingDistance;
            private List<AIBehavior> _units = new List<AIBehavior>();
            private Transform _transform;
            private EntityManager _entityManager;
            private UnitStats _unitStats;

            public void Init(Transform transform, EntityManager entityManager, UnitStats unitStats)
            {
                _transform = transform;
                _entityManager = entityManager;
                _unitStats = unitStats;
            }
            public void Tick()
            {
                DetectEntities();
            }

            private void DetectEntities()
            {
                var entities = _entityManager.GetAllEntities();

                foreach (var entity in entities)
                {
                    if (Vector3.Distance(_transform.position, entity.transform.position) > _unitStats.sensingDistance)
                    {
                        continue;
                    }
                    
                    var ai = entity.GetComponent<AIBehavior>();
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