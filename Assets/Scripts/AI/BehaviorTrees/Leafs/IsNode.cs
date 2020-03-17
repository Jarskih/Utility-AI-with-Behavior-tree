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
             case IsOp.Healthy:
                 retVal = agent.Owner.blackboard.memory[AIBlackboard.Keys.IsHurt] == false ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                 break;
             case IsOp.Wounded:
                 retVal = agent.Owner.blackboard.memory[AIBlackboard.Keys.IsHurt] == true ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                 break;
             case IsOp.Routing:
                 retVal = agent.Owner.blackboard.memory[AIBlackboard.Keys.LowMorale] == true ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                 break;
             case IsOp.InRange:
                 retVal = agent.Owner.blackboard.memory[AIBlackboard.Keys.InRange] == true ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
                 break;
         }

         return retVal;
     }
}