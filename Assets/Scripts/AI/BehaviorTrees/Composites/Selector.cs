using System.Collections.Generic;
using BehaviorTrees;
using XNode;

[Composite(typeof(Selector))]
public class Selector : BehaviorTreeNode 
{
    [Input] public List<BehaviorTreeResult> inResults;

    public override BehaviorTreeResult Execute()
    {
        NodePort inPort = GetPort("inResults");
        
        // No input connection (fail early)
        if (inPort == null) return BehaviorTreeResult.Failure;
        
        List<NodePort> connections = inPort.GetConnections();

        // OR gate (first succeeding node returns success from selector)
        foreach (NodePort port in connections)
        {
            BehaviorTreeResult result = (BehaviorTreeResult)port.GetOutputValue();
            if (result == BehaviorTreeResult.Success) { return BehaviorTreeResult.Success; }
            if (result == BehaviorTreeResult.Running) { return BehaviorTreeResult.Running; }
        }
        // All results failed
        return BehaviorTreeResult.Failure;
    }
}