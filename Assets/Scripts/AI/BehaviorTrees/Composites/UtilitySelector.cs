using System.Collections.Generic;
using System.Linq;
using BehaviorTrees;
using UnityEngine;
using XNode;

[Composite(typeof(UtilitySelector))]
public class UtilitySelector : BehaviorTreeNode
{
    [Input] public List<BehaviorTreeResult> inResults;

    public override BehaviorTreeResult Execute()
    {
        NodePort inPort = GetPort("inResults");
        
        // No input connection (fail early)
        if (inPort == null) return BehaviorTreeResult.Failure;
        
        List<NodePort> connections = inPort.GetConnections();
        
        // Get action priorities and sort them
        var priorities = agent.Owner.utility.priorities;
        var list = priorities.ToList();
        
        // Sort in descending order
        list.Sort((a, b) => b.Value.CompareTo(a.Value));
        
        if (agent.Owner.name == "Archer")
        {
            foreach (var l in list)
            {
                Debug.Log("Key: " + l.Key + " " + " " + l.Value);
            }
        }
        
        for (int i = 0; i < list.Count; i++)
        {
            foreach (var port in connections)
            { 
                var utility = port.node as UtilityNode;
                if (utility?.decision == list[i].Key)
                {
                    BehaviorTreeResult result = (BehaviorTreeResult)port.GetOutputValue();
                    if (result == BehaviorTreeResult.Success || result == BehaviorTreeResult.Running)
                    {
                        agent.Owner.DebugState(utility.decision);
                        return result;
                    }
                }
            }
        }

        // All results failed
        return BehaviorTreeResult.Failure;
    }
}