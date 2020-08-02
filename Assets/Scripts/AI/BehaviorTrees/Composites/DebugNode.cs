using BehaviorTrees;
using UnityEngine;

[Composite(typeof(DebugNode))]
public class DebugNode : BehaviorTreeNode
{
    public string state;
    [Input] public BehaviorTreeResult inResult;

    public override BehaviorTreeResult Execute()
    {
        Debug.Log(state);
        Debug.Log(agent.Owner);
        return GetInputValue("inResult", BehaviorTreeResult.Success);
    }
}