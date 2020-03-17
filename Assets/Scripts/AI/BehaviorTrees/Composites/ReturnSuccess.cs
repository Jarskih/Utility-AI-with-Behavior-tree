using BehaviorTrees;

[Composite(typeof(ReturnSuccess))]
public class ReturnSuccess : BehaviorTreeNode
{
    [Input] public BehaviorTreeResult inResult;
    public override BehaviorTreeResult Execute()
    {
        BehaviorTreeResult result = GetInputValue("inResult", BehaviorTreeResult.Success);
        if (result == BehaviorTreeResult.Running)
        {
            return BehaviorTreeResult.Running;
        } 
        else return BehaviorTreeResult.Success;
    }
}