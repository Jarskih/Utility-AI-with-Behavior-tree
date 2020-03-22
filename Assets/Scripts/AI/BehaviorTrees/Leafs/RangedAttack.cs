using BehaviorTrees;

[Leaf(typeof(RangedAttack))]
public class RangedAttack : BehaviorTreeNode
{ 
    public override BehaviorTreeResult Execute()
    {
        agent.Owner.weapon.RangedAttack();
        return BehaviorTreeResult.Success;
    }
}