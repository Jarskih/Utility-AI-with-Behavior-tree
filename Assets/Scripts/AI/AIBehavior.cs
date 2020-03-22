using UnityEngine;
using System.Collections;
using BehaviorTrees;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BehaviorTreeManager))]
    [RequireComponent(typeof(BehaviorTreeAgent))]
    public class AIBehavior : MonoBehaviour, IDamageable, IEventSource
    {
        public AudioEvent hurt;
        public BehaviourTreeType behaviorTreeType;
        public int playerIndex => _playerIndex;
        public AIBehavior currentTarget => _currentTarget;
        public Weapon weapon => _weapon;
        public Stats stats => _stats;

        private AIBehavior _currentTarget;
        [SerializeField] private int _playerIndex;
        private bool hasDied;

        // Monobehaviors
        private Animator _animatorController;
        private NavMeshAgent _navMeshAgent;
        private BehaviorTreeManager _behaviorTreeManager;
        private BehaviorTreeAgent _agent;
        private EntityManager _entityManager;
        private Weapon _weapon;
        private AudioSourcePoolManager _audioSourcePoolManager;

        // AI components
        public AIBlackboard blackboard;
        [SerializeField] private Sense _sense = new Sense();
        [SerializeField] private Stats _stats = new Stats();

        public AIBehavior()
        {
            blackboard = new AIBlackboard(this);
        }

        private void Start()
        {
            _audioSourcePoolManager = FindObjectOfType<AudioSourcePoolManager>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animatorController = GetComponentInChildren<Animator>();
            _entityManager = FindObjectOfType<EntityManager>();
            _weapon = GetComponent<Weapon>();

            _agent = new BehaviorTreeAgent(this, _animatorController, _navMeshAgent);

            RuntimeBehaviourTreeData.RegisterAgentContext(behaviorTreeType, _agent);
            blackboard.Init();
            _weapon.Init();
            _sense.Init(transform);
            _stats.Init(_entityManager, this, blackboard);
        }

        private void Update()
        {
            _sense.Tick();
            _stats.Tick();
            blackboard.Tick(_sense);
        }

        public bool IsActive()
        {
            return _stats.health > 0;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
        public void ReduceMana(int value)
        {
            _stats.ReduceMana(value);
        }

        public void ReduceMorale(int value)
        {
            _stats.ReduceMorale(value);
        }

        public void Damaged(DamageEvent damageEvent)
        {
            _stats.ReduceHealth(damageEvent.damage);
            _animatorController.SetTrigger(AnimDefinitions.Damaged);
          //  _audioSourcePoolManager.PlayAudioEvent(hurt);
        }
    }
}