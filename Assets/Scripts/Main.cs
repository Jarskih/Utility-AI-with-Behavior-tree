using System.Collections;
using System.Collections.Generic;
using BehaviorTrees;
using UnityEngine;

public class Main : MonoBehaviour
{
    private BehaviorTreeManager _behaviorTreeManager;
    private RuntimeBehaviourTree _runtimeBehaviourTree;
    
    void Start()
    {
        _runtimeBehaviourTree = GetComponent<RuntimeBehaviourTree>();
        _runtimeBehaviourTree.Init();
        
        _behaviorTreeManager = gameObject.AddComponent<BehaviorTreeManager>();
        _behaviorTreeManager.Init();
    }
    
    void Update()
    {
        _behaviorTreeManager.Tick();
    }
}
