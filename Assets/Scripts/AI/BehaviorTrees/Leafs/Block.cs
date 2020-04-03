using UnityEngine;
using BehaviorTrees;

[Leaf(typeof(Block))]
public class Block : BehaviorTreeNode
{
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetTrigger(AnimDefinitions.ShoudBlock);
        return BehaviorTreeResult.Success;
    }
}