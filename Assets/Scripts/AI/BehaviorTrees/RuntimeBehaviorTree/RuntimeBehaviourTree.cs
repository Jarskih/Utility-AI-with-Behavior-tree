using System.Collections.Generic;
using BehaviorTrees;
using UnityEngine;
using XNode;

public class RuntimeBehaviourTree : MonoBehaviour
{
    public BehaviourTreeType behaviourTreeType;
    public BehaviorTreeGraph runtimeTree;
    BehaviorTreeNode rootNode;
    bool isValid = false;
    public void Init()
    {
        isValid = ValidateAndSetRootNode();
        if (isValid)
        {
            RuntimeBehaviourTreeData.RegisterBehaviourTree(this);
        }
    }

    public void RunBehaviourTree(BehaviorTreeAgentData agentData)
    {
        if (isValid)
        {
            if (agentData.HasRunningNodes(out var runningNode))
            {
                BehaviorTreeNode parentNode = runningNode.GetPort("outResult").Connection.node as BehaviorTreeNode;
                if (parentNode != null)
                {
                    parentNode.agent = agentData.owner; 
                }
                runningNode.GetPort("outResult").GetOutputValue();
            }
            else
            {
                rootNode.agent = agentData.owner;
                rootNode.GetInputValue("inResult", BehaviorTreeResult.Failure);
            }
        }
    }

    private bool ValidateAndSetRootNode()
    {
        if (runtimeTree == null)
        {
            Debug.LogWarning("runtimeTree is null - Behaviour tree will not run - Add a Behaviour Tree to the RuntimeBehaviourTree Script");
            return false;
        }

        List<BehaviorTreeNode> rootNodeList = new List<BehaviorTreeNode>();
        foreach (Node node in runtimeTree.nodes)
        {
            Root root = node as Root;
            if (root != null)
            {
                rootNodeList.Add(root);
            }
        }

        if (rootNodeList.Count != 1)
        {
            Debug.LogWarning("There is no root node or more than 1 root node in this behaviourTree - BehaviourTree will not run - Make sure there is exactly 1 BTRoot node in your graph");
            return false;
        }

        rootNode = rootNodeList[0];
        return true;
    }
}
