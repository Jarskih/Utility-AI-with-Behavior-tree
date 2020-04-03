using AI.Events;
using UnityEngine;
using BehaviorTrees;
using TMPro;

[Leaf(typeof(Taunt))]
public class Taunt : BehaviorTreeNode
{
    public override BehaviorTreeResult Execute()
    {
        if (agent.Owner.HasMana(agent.Owner.stats.TauntCost))
        {
            agent.Owner.Taunt();
            agent.animatorController.SetTrigger(AnimDefinitions.ShouldTaunt);
            return BehaviorTreeResult.Success;
        }
        return BehaviorTreeResult.Failure;
    }
}