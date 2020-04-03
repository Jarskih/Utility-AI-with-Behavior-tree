using BehaviorTrees;

[Leaf(typeof(SetMoveState))]
public class SetMoveState : BehaviorTreeNode
{
    public MovementState desiredMoveState;
    public float moveSpeed;
    public bool overrideSpeed;
    
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetInteger(AnimDefinitions.MovementState, (int)desiredMoveState);
        agent.navAgent.speed = overrideSpeed ? moveSpeed : agent.Owner.stats.moveSpeed;

        if (agent.navAgent.speed > 0)
        {
            return BehaviorTreeResult.Success;
        }

        if (agent.navAgent.hasPath)
        {
            agent.navAgent.ResetPath();
        }

        return BehaviorTreeResult.Success;
    }
}