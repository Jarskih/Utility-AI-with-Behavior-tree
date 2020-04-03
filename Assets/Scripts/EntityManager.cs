    using System.Collections.Generic;
    using AI;
    using UnityEngine;

    public class EntityManager : MonoBehaviour
    {
        List<AIBehavior> _behaviors = new List<AIBehavior>();
        public void Init()
        {
            var behaviors = FindObjectsOfType<AIBehavior>();
            foreach (var behavior in behaviors)
            {
                _behaviors.Add(behavior);
            }
        }

        public void AddEntity(AIBehavior entity)
        {
            if(_behaviors.Contains(entity)) return;
            
            _behaviors.Add(entity);
        }

        public List<AIBehavior> GetFriendlyEntities(int player)
        {
            List<AIBehavior> entities = new List<AIBehavior>();

            foreach (var entity in _behaviors)
            {
                if (entity.playerIndex == player)
                {
                    if (entity.IsActive())
                    {
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }

        public List<AIBehavior> GetEnemyEntities(int player)
        {
            List<AIBehavior> entities = new List<AIBehavior>();

            foreach (var entity in _behaviors)
            {
                if (entity.playerIndex != player)
                {
                    if (entity.IsActive())
                    {
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }

        public List<AIBehavior> GetAllEntities()
        {
           return _behaviors;
        }
    }