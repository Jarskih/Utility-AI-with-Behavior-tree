using AI;
using UnityEngine;
using BehaviorTrees;

[Leaf(typeof(Heal))]
public class Heal : BehaviorTreeNode
{
    public override BehaviorTreeResult Execute()
    {
        var blackboard = agent.Owner.blackboard;
        var owner = agent.Owner;

        if (agent.Owner.blackboard.GetStatValue(AIBlackboard.Keys.Mana) < agent.Owner.stats.HealingCost)
        {
            return BehaviorTreeResult.Failure;
        }

        // Only heal when anyone in party is not in full health
        if (blackboard.GetStatValue(AIBlackboard.Keys.Health) < agent.Owner.stats.maxHealth ||
            blackboard.friendlyTarget && blackboard.friendlyTarget.blackboard.GetStatValue(AIBlackboard.Keys.Health) <
            blackboard.friendlyTarget.stats.maxHealth)
        {
            agent.animatorController.SetTrigger(AnimDefinitions.ShouldHeal);
            if (blackboard.friendlyTarget)
            {
                owner.Heal(blackboard.friendlyTarget, owner.stats.Healing);
            }
            owner.Heal(owner, owner.stats.Healing);
            owner.ReduceMana(owner.stats.HealingCost);
            return BehaviorTreeResult.Success;
        }

        return BehaviorTreeResult.Failure;
    }
}