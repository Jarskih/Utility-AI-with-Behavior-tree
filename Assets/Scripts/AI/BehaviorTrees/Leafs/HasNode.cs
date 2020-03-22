using AI;
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
                case HasOp.PathToWaypoint:
                    if (!agent.navAgent.enabled)
                    {
                        result = BehaviorTreeResult.Failure;
                    }
                    else if (agent.navAgent.hasPath && PathIsWithinToleranceToTarget(agent.navAgent.destination))
                    {
                        result = BehaviorTreeResult.Success;
                    }
                    else
                    {
                        agent.navAgent.ResetPath();
                        result = BehaviorTreeResult.Failure;
                    }
                    break;
                case HasOp.PathToFriendlyTarget:
                    if (!agent.navAgent.enabled)
                    {
                        result = BehaviorTreeResult.Failure;
                    }
                    else if (agent.navAgent.hasPath && PathIsWithinToleranceToTarget(agent.Owner.blackboard.friendlyTarget.transform))
                    {
                        result = BehaviorTreeResult.Success;
                    }
                    else
                    {
                        agent.navAgent.ResetPath();
                        result = BehaviorTreeResult.Failure;
                    }
                    break;
                case HasOp.PathToEnemyTarget:
                    if (!agent.navAgent.enabled)
                    {
                        result = BehaviorTreeResult.Failure;
                    }
                    else if (agent.navAgent.hasPath && PathIsWithinToleranceToTarget(agent.Owner.blackboard.enemyTarget.transform))
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

        private bool PathIsWithinToleranceToTarget(Transform target)
        {
            if (target)
            {
                return (agent.navAgent.pathEndPosition - target.position).sqrMagnitude < destinationTolerance * destinationTolerance;
            }
            return true;
        }
        
        private bool PathIsWithinToleranceToTarget(Vector3 target)
        {
            return (agent.navAgent.pathEndPosition - target).sqrMagnitude < destinationTolerance * destinationTolerance;
        }
    }