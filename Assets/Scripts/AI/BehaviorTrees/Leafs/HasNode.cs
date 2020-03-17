using BehaviorTrees;
using UnityEngine;

[Leaf(typeof(HasNode))]
    public class HasNode : BehaviorTreeNode
    {
        public HasOp operation;
        public float destinationTolerance = 1;

        public override BehaviorTreeResult Execute()
        {
            BehaviorTreeResult result = BehaviorTreeResult.Failure;
            switch (operation)
            {
                case HasOp.Path:
                    result = agent.navAgent.hasPath ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                    break;
                case HasOp.PathToTarget:
                    if (!agent.navAgent.enabled)
                    {
                        result = BehaviorTreeResult.Failure;
                    }
                    else if (agent.navAgent.hasPath && PathIsWithinToleranceToTarget())
                    {
                        result = BehaviorTreeResult.Success;
                    }
                    else
                    {
                        agent.navAgent.ResetPath();
                        result = BehaviorTreeResult.Failure;
                    }
                    break;
                case HasOp.EnemyTarget:
                    result = agent.Owner.blackboard.enemyTarget != null ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                    break;
                case HasOp.FriendlyTarget:
                    result = agent.Owner.blackboard.friendlyTarget != null ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                    break;
            }
            return result;
        }

        private bool PathIsWithinToleranceToTarget()
        {
            return (agent.navAgent.pathEndPosition - agent.Owner.currentTarget.GetPosition()).sqrMagnitude < destinationTolerance * destinationTolerance;
        }
    }