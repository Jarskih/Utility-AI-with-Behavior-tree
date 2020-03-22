using System.Collections;
using System.Collections.Generic;
using BehaviorTrees;
using UnityEngine;

public class Main : MonoBehaviour
{
    private AudioSourcePoolManager _audioSourcePoolManager;
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

        _audioSourcePoolManager = gameObject.AddComponent<AudioSourcePoolManager>();
    }
    
    void Update()
    {
        _behaviorTreeManager.Tick();
    }
}
