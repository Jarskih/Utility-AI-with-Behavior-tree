using AI;
using UnityEngine;
using BehaviorTrees;

[Leaf(typeof(Heal))]
public class Heal : BehaviorTreeNode
{
    public override BehaviorTreeResult Execute()
    {
        var blackboard = agent.Owner.blackboard;

        if (agent.Owner.HasMana(agent.Owner.stats.HealingCost))
        {
            return BehaviorTreeResult.Failure;
        }
        
        if (blackboard.GetStatValue(AIBlackboard.Keys.FriendHealth) < blackboard.friendlyTarget.stats.maxHealth)
        {
            agent.animatorController.SetTrigger(AnimDefinitions.ShouldHeal);
            agent.Owner.Heal(blackboard.friendlyTarget, agent.Owner.stats.Healing);
            return BehaviorTreeResult.Success;
        }

        if (blackboard.GetStatValue(AIBlackboard.Keys.Health) < agent.Owner.stats.maxHealth)
        {
            agent.animatorController.SetTrigger(AnimDefinitions.ShouldHeal);
            agent.Owner.Heal(agent.Owner, agent.Owner.stats.Healing);
            return BehaviorTreeResult.Success;
        }

        return BehaviorTreeResult.Failure;
    }
}