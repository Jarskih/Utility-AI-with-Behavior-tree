using System.Collections;
using System.Collections.Generic;
using AI;
using AI.Events;
using BehaviorTrees;
using UnityEngine;

public class Main : MonoBehaviour
{
    private GameObject _monsterPrefab;
    private GameObject _paladinPrefab;
    private GameObject _archerPrefab;
        
    private AudioSourcePoolManager _audioSourcePoolManager;
    private EntityManager _entityManager;
    private BehaviorTreeManager _behaviorTreeManager;
    private List<RuntimeBehaviourTree> _runtimeBehaviourTree = new List<RuntimeBehaviourTree>();
    private AIEventSystem _aiEventSystem;
    private MonsterSpawner _monsterSpawner;
    
    private List<AIBehavior> _entities = new List<AIBehavior>();

    void Awake()
    {
        _monsterPrefab = Resources.Load<GameObject>("Prefabs/Monster");
        _paladinPrefab = Resources.Load<GameObject>("Prefabs/Paladin");
        _archerPrefab = Resources.Load<GameObject>("Prefabs/Archer");

        foreach (var tree in GetComponents<RuntimeBehaviourTree>())
        {
            _runtimeBehaviourTree.Add(tree);
            tree.Init();
        }


        _aiEventSystem = GetComponent<AIEventSystem>();
        _aiEventSystem.stimData = Resources.Load<AIStimData>("EventData/AIStimData");
        
        _monsterSpawner = new MonsterSpawner();
        _behaviorTreeManager = gameObject.AddComponent<BehaviorTreeManager>();
        _entityManager = gameObject.AddComponent<EntityManager>();
        
        _monsterSpawner.Init(_monsterPrefab, _aiEventSystem, _entityManager);
        _behaviorTreeManager.Init();
        _entityManager.Init();

        _entities = _entityManager.GetAllEntities();
        foreach (var entity in _entities)
        {
            entity.Init(_aiEventSystem);
        }

        _audioSourcePoolManager = gameObject.AddComponent<AudioSourcePoolManager>();
    }
    
    void Update()
    {
        _monsterSpawner.Tick();
        _behaviorTreeManager.Tick();
        foreach (var entity in _entities)
        {
            entity.Tick();
        }
    }
}
