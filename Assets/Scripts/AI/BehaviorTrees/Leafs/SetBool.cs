using BehaviorTrees;

[Leaf(typeof(SetBool))]
public class SetBool : BehaviorTreeNode
{
    public string boolName;
    public bool value;
    
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetBool(boolName, value);
        return BehaviorTreeResult.Success;
    }
}