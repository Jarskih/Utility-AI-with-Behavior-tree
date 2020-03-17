using BehaviorTrees;

[Composite(typeof(Invert))]
public class Invert : BehaviorTreeNode
{
	[Input] public BehaviorTreeResult inResult;

	public override BehaviorTreeResult Execute()
	{
		BehaviorTreeResult result = GetInputValue("inResult", BehaviorTreeResult.Success);
		
		// Do not invert if running
		if (result == BehaviorTreeResult.Running)
		{
			return BehaviorTreeResult.Running;
		}

		// Invert result
		return result == BehaviorTreeResult.Success ? BehaviorTreeResult.Failure : BehaviorTreeResult.Success;
	}
}