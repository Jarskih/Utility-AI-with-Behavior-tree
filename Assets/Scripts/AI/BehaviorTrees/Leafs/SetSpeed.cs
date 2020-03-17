using BehaviorTrees;

[Leaf(typeof(SetSpeed))]
public class SetSpeed : BehaviorTreeNode
{
    public float desiredSpeed;
    public override BehaviorTreeResult Execute()
    {
        agent.navAgent.speed = desiredSpeed;
        return BehaviorTreeResult.Success;
    }
}