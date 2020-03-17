using System.Collections.Generic;
using BehaviorTrees;
using XNode;

[Composite(typeof(Sequence))]
public class Sequence : BehaviorTreeNode 
{
    [Input] public List<BehaviorTreeResult> inResults;

    public override BehaviorTreeResult Execute()
    {
        NodePort inPort = GetPort("inResults");
        
        // No input connection (fail early)
        if (inPort == null) return BehaviorTreeResult.Failure;
        
        List<NodePort> connections = inPort.GetConnections();

        // AND gate (all results must succeed to return success)
        foreach (NodePort port in connections)
        {
            BehaviorTreeResult result = (BehaviorTreeResult)port.GetOutputValue();
            if (result == BehaviorTreeResult.Failure) { return BehaviorTreeResult.Failure; }
            if (result == BehaviorTreeResult.Running) { return BehaviorTreeResult.Running; }
        }
        // All results succeeded
        return BehaviorTreeResult.Success;
    }
}