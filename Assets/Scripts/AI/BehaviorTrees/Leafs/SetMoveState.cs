using BehaviorTrees;

[Leaf(typeof(SetMoveState))]
public class SetMoveState : BehaviorTreeNode
{
    public MovementState desiredMoveState;
    public float moveSpeed;

    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetInteger(AnimDefinitions.MovementState, (int)desiredMoveState);
        agent.navAgent.speed = moveSpeed;

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