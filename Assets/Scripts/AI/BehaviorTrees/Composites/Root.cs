using BehaviorTrees;
using XNode;

[Composite(typeof(Root))]
public class Root : BehaviorTreeNode
{
    [Input] public BehaviorTreeNode inResult;

    public override object GetValue(NodePort port)
    {
        return Execute();
    }

    public override BehaviorTreeResult Execute()
    {
        return GetInputValue("inResult", BehaviorTreeResult.Failure);
    }
}