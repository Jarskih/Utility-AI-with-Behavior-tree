using BehaviorTrees;
using UnityEngine;

[Leaf(typeof(TurnTo))]
public class TurnTo : BehaviorTreeNode
{
    public TurnTarget turnTarget;
    public override BehaviorTreeResult Execute()
    {
        var result = BehaviorTreeResult.Failure;
        Transform target = null;
        switch (turnTarget)
        {
            case TurnTarget.Enemy:
                target = agent.Owner.blackboard.enemyTarget?.transform;
                break;
            case TurnTarget.Friendly:
                target = agent.Owner.blackboard.friendlyTarget?.transform;
                break;
            case TurnTarget.Forward:
                agent.Owner.GetComponentInChildren<Animator>().transform.localRotation = Quaternion.identity;
                result = BehaviorTreeResult.Success;
                break;
        }

        if (target)
        {
            var dir = target.position - agent.Owner.transform.position;
            agent.Owner.GetComponentInChildren<Animator>().transform.rotation = Quaternion.LookRotation(dir.normalized);
            result = BehaviorTreeResult.Success;
        }

        return result;
    }
}