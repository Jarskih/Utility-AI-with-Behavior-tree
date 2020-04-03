using System;
using UnityEngine;
using System.Collections;
using AI.Events;
using BehaviorTrees;
using UnityEngine.AI;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BehaviorTreeManager))]
    [RequireComponent(typeof(BehaviorTreeAgent))]
    public class AIBehavior : MonoBehaviour, IDamageable, IEventSource, IHealable
    {
        // What type of AI this is (behavior tree it uses)
        public BehaviourTreeType behaviorTreeType;
        public int playerIndex => _playerIndex;
        public Weapon weapon => _weapon;
        public Stats stats => _stats;
        public UtilityFunctions utility => _utility;

        [SerializeField] private int _playerIndex;
        [SerializeField] private UnitStats unitStats;
        [SerializeField] GameObject[] bloodArray;
        [SerializeField] AudioEvent _hurtSound;

        // Monobehaviors
        private Animator _animatorController;
        private NavMeshAgent _navMeshAgent;
        private BehaviorTreeManager _behaviorTreeManager;
        private BehaviorTreeAgent _agent;
        private EntityManager _entityManager;
        private Weapon _weapon;
        private AudioSourcePoolManager _audioSourcePoolManager;
        private ParticleSystem _blood;
        private ParticleSystem _healParticleSystem;
        private CapsuleCollider _capsuleCollider;
        private Waypoints _waypoints;

        // AI components
        public AIBlackboard blackboard;
        public AIEventSystem eventSystem;
        private Sense _sense = new Sense();
        private Stats _stats = new Stats();
        [SerializeField] private UtilityFunctions _utility = new UtilityFunctions();

        // AI decision limiter
        private float counter = 1;
        private float updateTime = 0.2f;
        
        // UI
        public FloatVariable UIhealth;
        public FloatVariable UImana;
        
        private WaitForSeconds waitFor = new WaitForSeconds(0.2f);
        private UtilityFunctions.Decisions _currentDecision;
        private Waypoint _currentWaypoint;
        private bool _init;
        private bool hasDied;

        public void Init(AIEventSystem eventSystem)
        {
            blackboard = new AIBlackboard(this, _utility);
            
            // Listen for events from other AI agents
            this.eventSystem = eventSystem;
            this.eventSystem.AIEvent += OnEvent;
            
            _audioSourcePoolManager = FindObjectOfType<AudioSourcePoolManager>();
            _entityManager = FindObjectOfType<EntityManager>();

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animatorController = GetComponentInChildren<Animator>();
            _blood = GetComponentInChildren<BloodParticles>().GetComponent<ParticleSystem>();
            _healParticleSystem = GetComponent<ParticleSystem>();
            _capsuleCollider = GetComponent<CapsuleCollider>();

            _waypoints = FindObjectOfType<Waypoints>();
            _currentWaypoint = _waypoints.GetFirstWaypoint();

            _weapon = GetComponent<Weapon>();

            _agent = new BehaviorTreeAgent(this, _animatorController, _navMeshAgent);

            RuntimeBehaviourTreeData.RegisterAgentContext(behaviorTreeType, _agent);
            blackboard.Init();
            _weapon.Init();
            _sense.Init(transform, _entityManager, unitStats);
            _stats.Init(_entityManager, this, blackboard, unitStats);
            _utility.Init();

            _init = true;
        }

        public void Tick()
        {
            if (!_init) return;
            if (!IsActive())
            {
                _navMeshAgent.enabled = false;
                _capsuleCollider.enabled = false;
            }

            _stats.Tick();
            counter += Time.deltaTime;

            if (counter > updateTime)
            {
                _sense.Tick();
                blackboard.Tick(_sense);
                counter = 0;
            }
            
            
            // Update UI
            if (UIhealth && UImana)
            {
                UIhealth.Value = blackboard.GetStatValue(AIBlackboard.Keys.Health);
                UImana.Value = blackboard.GetStatValue(AIBlackboard.Keys.Mana);
            }
        }
        
        private void OnEvent(AIEventData eventData)
        {
            var owner = eventData.owner;
            
            switch (eventData.type)
            {
                case AIEventType.Hurt:
                    if (_playerIndex == owner._playerIndex)
                    {
                        blackboard.SetEnemyTarget(owner.blackboard.enemyTarget);
                    }
                    break;
                case AIEventType.Taunt:
                    if (_playerIndex != owner._playerIndex)
                    {
                        blackboard.SetEnemyTarget(owner);
                        Debug.Log("Taunt");
                    }
                    break;
            }
        }

        public bool IsActive()
        {
            return blackboard.GetStatValue(AIBlackboard.Keys.Health) > 0;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Damaged(DamageEvent damageEvent)
        {
            blackboard.UpdateBlackBoard(AIBlackboard.Keys.Health, -damageEvent.damage);
            StartCoroutine(PlayAndStopParticleSystem(_blood));
            Instantiate(GetRandomBlood(), transform.position, Quaternion.identity);
            _audioSourcePoolManager.PlayAudioEvent(_hurtSound);
            
            // Set new target when attacked
            if (blackboard.enemyTarget == null) {
                blackboard.SetEnemyTarget(damageEvent.attacker);
            }
        }

        private GameObject GetRandomBlood()
        {
            var random = Random.Range(0, bloodArray.Length);
            return bloodArray[random];
        }

        private IEnumerator PlayAndStopParticleSystem(ParticleSystem p)
        {
            p.Play();
            yield return waitFor;
            p.Stop();
        }
        
        private void OnDisable()
        {
            if (eventSystem)
            {
                eventSystem.AIEvent -= OnEvent;
            }
        }

        #region Abilities
        
        public void Heal(AIBehavior friendlyTarget, int value)
        {
            friendlyTarget.Healed(value);
        }
        
        public void Taunt()
        {
            eventSystem.TriggerEvent( this, null, AIEventType.Taunt);
        }

        public void ReduceMana(int value)
        {
            blackboard.UpdateBlackBoard(AIBlackboard.Keys.Mana, -value);
        }

        private void Healed(int heal)
        {
            var health = blackboard.GetStatValue(AIBlackboard.Keys.Health) + heal;
            health = Mathf.Clamp(health, 0, stats.maxHealth);
            blackboard.SetBlackboardValue(AIBlackboard.Keys.Health, health);
            StartCoroutine(PlayAndStopParticleSystem(_healParticleSystem));
        }
        
        public bool HasMana(float cost)
        {
            var manaLeft = blackboard.GetStatValue(AIBlackboard.Keys.Mana);
            if (manaLeft < cost)
            {
                return false;
            }
            return true;
        }
        
        #endregion

        public Vector3 GetWaypoint()
        {
            if (Vector3.Distance(_currentWaypoint.transform.position, transform.position) < 2)
            {
                _currentWaypoint =  _waypoints.GetNextWayPoint(_currentWaypoint.index);
            }
            return _currentWaypoint.transform.position;
        }

        #region Debug
        public void DebugState(UtilityFunctions.Decisions utilityDecision)
        {
            _currentDecision = utilityDecision;
        }

        public Vector3 retreatTarget;

        private void OnDrawGizmos()
        {
            switch (_currentDecision)
            {
                case UtilityFunctions.Decisions.ShouldAttack:
                    Gizmos.color = Color.red;
                    break;
                case UtilityFunctions.Decisions.ShouldBlock:
                    Gizmos.color = Color.blue;
                    break;
                case UtilityFunctions.Decisions.ShouldTaunt:
                case UtilityFunctions.Decisions.ShouldHeal:
                    Gizmos.color = Color.green;
                    break;
                case UtilityFunctions.Decisions.ShouldRetreat:
                    Gizmos.color = Color.yellow;
                    break;
            }
            
            Gizmos.DrawWireCube(transform.position, Vector3.one);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(retreatTarget, Vector3.one);
        }

        private void OnDrawGizmosSelected()
        {
            if (_currentWaypoint == null) return;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, _currentWaypoint.transform.position);
        }

        #endregion


    }
}