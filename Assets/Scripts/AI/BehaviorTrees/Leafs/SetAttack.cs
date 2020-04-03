using UnityEngine;
using BehaviorTrees;

[Leaf(typeof(SetAttack))]
public class SetAttack : BehaviorTreeNode
{
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetTrigger(AnimDefinitions.ShouldAttack);
        return BehaviorTreeResult.Success;
    }
}