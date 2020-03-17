using System.Collections;
using System.Collections.Generic;
using BehaviorTrees;
using UnityEngine;

public class Main : MonoBehaviour
{
    private EntityManager _entityManager;
    private BehaviorTreeManager _behaviorTreeManager;
    private List<RuntimeBehaviourTree> _runtimeBehaviourTree = new List<RuntimeBehaviourTree>();
    
    void Awake()
    {
        foreach (var tree in GetComponents<RuntimeBehaviourTree>())
        {
            _runtimeBehaviourTree.Add(tree);
            tree.Init();
        }

        _behaviorTreeManager = gameObject.AddComponent<BehaviorTreeManager>();
        _behaviorTreeManager.Init();

        _entityManager = gameObject.AddComponent<EntityManager>();
        _entityManager.Init();
    }
    
    void Update()
    {
        _behaviorTreeManager.Tick();
    }
}
