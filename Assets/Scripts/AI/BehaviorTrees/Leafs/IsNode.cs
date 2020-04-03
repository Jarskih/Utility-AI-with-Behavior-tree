using AI;
using BehaviorTrees;
using UnityEngine;

[Leaf(typeof(IsNode))]
 public class IsNode : BehaviorTreeNode
 {
     public IsOp isOperation;

     public override BehaviorTreeResult Execute()
     {
         BehaviorTreeResult retVal = BehaviorTreeResult.Failure;
         switch (isOperation)
         {
             case IsOp.Wounded:
                 retVal = agent.Owner.blackboard.GetMemoryValue(AIBlackboard.Keys.IsHurt) ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                 break;
             case IsOp.LowMorale:
                 retVal = agent.Owner.blackboard.GetMemoryValue(AIBlackboard.Keys.LowMorale) ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                 break;
             case IsOp.InAttackRange:
                 retVal = agent.Owner.blackboard.GetMemoryValue(AIBlackboard.Keys.InRange) ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                 break;
             case IsOp.Dead:
                 retVal = agent.Owner.blackboard.GetMemoryValue(AIBlackboard.Keys.IsDead) ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                 break;
         }

         return retVal;
     }
}