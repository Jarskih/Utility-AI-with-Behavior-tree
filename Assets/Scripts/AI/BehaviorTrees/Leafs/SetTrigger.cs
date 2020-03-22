using BehaviorTrees;

[Leaf(typeof(SetTrigger))]
public class SetTrigger : BehaviorTreeNode
{
    public string TriggerName;
    
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetTrigger(TriggerName);
        return BehaviorTreeResult.Success;
    }
}