using UnityEngine;
using BehaviorTrees;

[Leaf(typeof(SetAttack))]
public class SetAttack : BehaviorTreeNode
{
    public bool ShouldAttack;
    
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetBool(AnimDefinitions.ShouldAttack, ShouldAttack);
        return BehaviorTreeResult.Success;
    }
}