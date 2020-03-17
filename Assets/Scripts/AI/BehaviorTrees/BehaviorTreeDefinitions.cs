using System;
using System.Reflection;
using XNode;

namespace BehaviorTrees
{
    public static class AnimDefinitions
    {
        public static string ShouldAttack = "Attack";
        public const string MovementState = "MoveState";
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
        Friendly
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
        TARGET,
        RANDOM,
        EnemyTarget,
        FriendlyTarget,
    }
    
    public enum HasOp
    {
        Path,
        PathToTarget,
        FriendlyTarget,
        EnemyTarget
    }

    public enum IsOp
    {
        Healthy,
        Wounded,
        Routing,
        InRange
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