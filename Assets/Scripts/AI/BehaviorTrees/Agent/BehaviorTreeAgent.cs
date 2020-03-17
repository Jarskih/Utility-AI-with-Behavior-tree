using System;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTrees
{
    [Serializable]
    public class BehaviorTreeAgent
    {
        public AIBehavior Owner { get; }
        public Animator animatorController;
        private NavMeshAgent _navAgent;
        public NavMeshAgent navAgent => _navAgent;
        public BehaviorTreeAgent(AIBehavior owner, Animator animatorController, NavMeshAgent navAgent)
        {
            Owner = owner;
            this.animatorController = animatorController;
            _navAgent = navAgent;
        }
    }
}