using System.Collections.Generic;
using BehaviorTrees;
using UnityEngine;
using XNode;

[Composite(typeof(SubTree))]
public class SubTree : BehaviorTreeNode
{
    public BehaviorTreeGraph subTree;
    Root subTreeRoot;

    bool isValid = false;
    public override void OnInit()
    {
        isValid = ValidateAndSetRootNode();
    }

    public override BehaviorTreeResult Execute()
    {
        if (!isValid)
        {
            return BehaviorTreeResult.Failure;
        }
        else
        {
            subTreeRoot.agent = agent;
            return subTreeRoot.GetInputValue("inResult", BehaviorTreeResult.Failure);
        }
    }

    private bool ValidateAndSetRootNode()
    {
        if (subTree == null)
        {
            Debug.LogWarning("subTree is null - tree will not run");
            return false;
        }

        List<Root> rootNodeList = new List<Root>();
        foreach (Node node in subTree.nodes)
        {
            Root root = node as Root;
            if (root != null)
            {
                rootNodeList.Add(root);
            }
        }

        if (rootNodeList.Count != 1)
        {
            Debug.LogWarning("There is no root node or more than 1 root node in this subTree - subtree will not run - Make sure there is exactly 1BTRoot node in your graph");
            return false;
        }

        subTreeRoot = rootNodeList[0];

        return true;
    }
}