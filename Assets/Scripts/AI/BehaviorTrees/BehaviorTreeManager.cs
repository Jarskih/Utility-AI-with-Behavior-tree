using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTrees
{
    public class BehaviorTreeManager : MonoBehaviour
    {
        private BehaviorTreeGraph _graph;
        private Dictionary<BehaviourTreeType, RuntimeBehaviourTree> _behaviourTreeMap = new Dictionary<BehaviourTreeType, RuntimeBehaviourTree>();
        private Dictionary<BehaviourTreeType, List<BehaviorTreeAgentData>> _agents = new Dictionary<BehaviourTreeType, List<BehaviorTreeAgentData>>();
        
        public void Register(BehaviourTreeType behaviorTreeType, BehaviorTreeAgentData agent)
        {
            if (_agents.ContainsKey(behaviorTreeType))
            {
                _agents[behaviorTreeType].Add(agent);
            }
            else
            {
                _agents[behaviorTreeType] = new List<BehaviorTreeAgentData> {agent};
            }
        }

        public void Init()
        {
            _behaviourTreeMap = RuntimeBehaviourTreeData.GetBehaviourTrees();
            _agents = RuntimeBehaviourTreeData.GetContextData();
            InitializeAllBehaviourTrees();
        }

        public void Tick()
        {
            RunAgents();
        }

        void RunAgents()
        {
            for (int i = 0; i < (int)BehaviourTreeType.Count; ++i)
            {
                BehaviourTreeType treeType = (BehaviourTreeType)i;

                if (_behaviourTreeMap.ContainsKey(treeType))
                {
                    if (_agents.ContainsKey(treeType))
                    {
                        _agents[treeType].ForEach(x => _behaviourTreeMap[treeType].RunBehaviourTree(x));
                    }
                }
            }
        }
        
        void InitializeAllBehaviourTrees()
        {
            for (int i = 0; i < (int)BehaviourTreeType.Count; ++i)
            {
                BehaviourTreeType treeType = (BehaviourTreeType)i;

                if (_behaviourTreeMap.ContainsKey(treeType))
                {
                    InitializeTreeNodes(_behaviourTreeMap[treeType].runtimeTree.nodes); 
                }
            }
        }
        
        void InitializeTreeNodes(List<Node> nodeList) 
        {
            foreach (Node node in nodeList)
            {
                BehaviorTreeNode currentNode = (BehaviorTreeNode)node;
                if (currentNode != null)
                {
                    currentNode.OnInit();
                }
            }
        }
    }
}