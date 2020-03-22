using System;
using System.Reflection;
using XNode;

namespace BehaviorTrees
{
    public static class AnimDefinitions
    {
        public static string ShouldAttack = "Attack";
        public static string IsDead = "IsDead";
        public static string AttackType = "AttackType";
        public static string MovementState = "MoveState";
        public static string Damaged = "Damaged";
    }
    
    public enum MovementState
    {
        Idle,
        Walk,
        Run,
    }

    public enum TurnTarget
    {
        Enemy,
        Friendly,
        Forward
    }
    
    public enum BehaviorTreeResult
    {
        Success,
        Failure,
        Running,
    }
    public enum BehaviourTreeType
    {
        Archer,
        Paladin,
        Monster,
        Count, 
    }
    
    public enum PathType
    {
        Target,
        Random,
        EnemyTarget,
        FriendlyTarget,
        Retreat
    }
    
    public enum HasOp
    {
        Path,
        PathToWaypoint,
        PathToFriendlyTarget,
        FriendlyTarget,
        EnemyTarget,
        PathToEnemyTarget
    }

    public enum IsOp
    {
        Healthy,
        Wounded,
        Routing,
        InAttackRange,
        Dead
        
    }

    public class CompositeAttribute : Node.CreateNodeMenuAttribute
    {
        public CompositeAttribute(Type type) : base("")
        {
            menuName = "Composites/" + type.ToString();
        }
    }
    
    public class LeafAttribute : Node.CreateNodeMenuAttribute
    {
        public LeafAttribute(Type type) : base("")
        {
            menuName = "Leafs/" + type.ToString();
        }
    }
}