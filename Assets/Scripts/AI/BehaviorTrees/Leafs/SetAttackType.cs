using UnityEngine;
using BehaviorTrees;

[Leaf(typeof(SetAttackType))]
public class SetAttackType : BehaviorTreeNode
{
    public int attackType;
    public bool isRandom;
    public int min;
    public int max;

    public override BehaviorTreeResult Execute()
    {
        if (isRandom)
        {
            var randomInt = Random.Range(0, max + 1);
            agent.animatorController.SetInteger(AnimDefinitions.AttackType, randomInt);
        }
        else
        {
            agent.animatorController.SetInteger(AnimDefinitions.AttackType, attackType);
        }
        return BehaviorTreeResult.Success;
    }
}