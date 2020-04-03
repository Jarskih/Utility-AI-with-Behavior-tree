using AI;
using BehaviorTrees;
using UnityEngine;

[Leaf(typeof(Damaged))]
public class Damaged : BehaviorTreeNode
{
    public override BehaviorTreeResult Execute()
    {
        agent.animatorController.SetTrigger(AnimDefinitions.Damaged);
        agent.Owner.blackboard.UpdateBlackBoard(AIBlackboard.Keys.IsHurt, false);
        return BehaviorTreeResult.Success;
    }
}