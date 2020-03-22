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
        if (!agent.animatorController.applyRootMotion)
        {
            agent.navAgent.speed = overrideSpeed ? moveSpeed : agent.Owner.stats.moveSpeed;
        }
        else
        {
            agent.navAgent.speed = 0;
        }
        return BehaviorTreeResult.Success;
    }
}