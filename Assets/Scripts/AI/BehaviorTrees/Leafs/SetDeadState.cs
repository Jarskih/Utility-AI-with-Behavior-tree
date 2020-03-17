using BehaviorTrees;

[Leaf(typeof(SetDeadState))]
public class SetDeadState : BehaviorTreeNode
{
    public bool state;
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetBool(AnimDefinitions.IsDead, state);
        return BehaviorTreeResult.Success;
    }
}