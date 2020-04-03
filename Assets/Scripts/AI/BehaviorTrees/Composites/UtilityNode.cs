using BehaviorTrees;

[Composite(typeof(UtilityNode))]
public class UtilityNode : BehaviorTreeNode
{
    [Input] public BehaviorTreeResult inResult;
    
    // Choose which action utility will be calculated
    public UtilityFunctions.Decisions decision;
    public override BehaviorTreeResult Execute()
    {
        return GetInputValue("inResult", BehaviorTreeResult.Failure);
    }
}
