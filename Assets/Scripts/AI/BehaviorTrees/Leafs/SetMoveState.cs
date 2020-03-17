using BehaviorTrees;

[Leaf(typeof(SetMoveState))]
public class SetMoveState : BehaviorTreeNode
{
    public MovementState desiredMoveState;
    public float moveSpeed;
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetInteger(AnimDefinitions.MovementState, (int)desiredMoveState);
        if (!agent.animatorController.applyRootMotion)
        {
            agent.navAgent.speed = moveSpeed;
        }
        else
        {
            agent.navAgent.speed = 0;
        }
        return BehaviorTreeResult.Success;
    }
}