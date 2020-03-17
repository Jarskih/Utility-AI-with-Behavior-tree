using System.Collections.Generic;
using BehaviorTrees;

public static class RuntimeBehaviourTreeData
{
    static readonly Dictionary<BehaviourTreeType, RuntimeBehaviourTree> behaviourTreeMap = new Dictionary<BehaviourTreeType, RuntimeBehaviourTree>();
    static readonly Dictionary<BehaviourTreeType, List<BehaviorTreeAgentData>> agents = new Dictionary<BehaviourTreeType, List<BehaviorTreeAgentData>>();
    
    public static void RegisterBehaviourTree(RuntimeBehaviourTree behaviourTree)
    {
        behaviourTreeMap[behaviourTree.behaviourTreeType] = behaviourTree;
    }

    public static void RegisterAgentContext(BehaviourTreeType behaviourTree, BehaviorTreeAgent agent)
    {
        if (!agents.ContainsKey(behaviourTree))
        {
            agents[behaviourTree] = new List<BehaviorTreeAgentData>();
        }

        agents[behaviourTree].Add(new BehaviorTreeAgentData(agent));
    }

    public static void AddRunningNode(BehaviorTreeAgent agent, BehaviorTreeNode node)
    {
        BehaviorTreeAgentData data = agents[agent.Owner.behaviorTreeType].Find(x => x.owner == agent);
        data?.AddRunningNode(node);
    }

    public static void RemoveRunningNode(BehaviorTreeAgent agent, BehaviorTreeNode _node)
    {
        BehaviorTreeAgentData data = agents[agent.Owner.behaviorTreeType].Find(x => x.owner == agent);
        data?.RemoveRunningNode(_node);
    }

    public static Dictionary<BehaviourTreeType, RuntimeBehaviourTree> GetBehaviourTrees()
    {
        return behaviourTreeMap;
    }

    public static Dictionary<BehaviourTreeType, List<BehaviorTreeAgentData>> GetContextData()
    {
        return agents;
    }
}