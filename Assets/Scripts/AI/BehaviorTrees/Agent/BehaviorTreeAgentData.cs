using UnityEngine;
using System.Collections.Generic;

namespace BehaviorTrees
{
    public class BehaviorTreeAgentData
    {
        public BehaviorTreeAgentData(BehaviorTreeAgent owner)
        {
            this.owner = owner;
        }

        public readonly BehaviorTreeAgent owner;
        readonly List<BehaviorTreeNode> runningNodes = new List<BehaviorTreeNode>();

        public void AddRunningNode(BehaviorTreeNode node)
        {
            if (!runningNodes.Contains(node))
            {
                runningNodes.Insert(0, node);
            }
        }

        public void RemoveRunningNode(BehaviorTreeNode node)
        {
            runningNodes.Remove(node);
        }

        public bool HasRunningNodes(out BehaviorTreeNode runningNode)
        {
            runningNode = null;

            if (runningNodes.Count != 0)
            {
                runningNode = runningNodes[0];
                return true;
            }

            return false;
        }
    }
}