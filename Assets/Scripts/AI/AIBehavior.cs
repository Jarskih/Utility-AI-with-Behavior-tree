using BehaviorTrees;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BehaviorTreeManager))]
    [RequireComponent(typeof(BehaviorTreeAgent))]
    public class AIBehavior : MonoBehaviour, IDamageable
    {
        public BehaviourTreeType behaviorTreeType = BehaviourTreeType.Archer;
        public AIBehavior currentTarget => _currentTarget;
        
        [SerializeField] private AIBehavior _currentTarget;

        public int playerIndex = 0;

        // Monobehaviors
        private Animator _animatorController;
        private NavMeshAgent _navMeshAgent;
        private BehaviorTreeManager _behaviorTreeManager;
        private BehaviorTreeAgent _agent;

        // AI components
        public AIBlackboard blackboard;
        [SerializeField] private Sense _sense = new Sense();
        [SerializeField] private Stats _stats = new Stats();

        public AIBehavior()
        {
            blackboard = new AIBlackboard(this);
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animatorController = GetComponentInChildren<Animator>();

            _agent = new BehaviorTreeAgent(this, _animatorController, _navMeshAgent);
        }

        private void Start()
        {
            RuntimeBehaviourTreeData.RegisterAgentContext(behaviorTreeType, _agent);
            blackboard.Init();
            _sense.Init(transform);
            _stats.Init();
        }

        private void Update()
        {
            if (!IsActive())
            {
                return;
            }
            _sense.Tick();
            _stats.Tick();
            blackboard.Tick(_sense, _stats);
        }
        
        public bool IsActive()
        {
            return _stats.health > 0;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Damaged()
        {
            
        }
    }
}