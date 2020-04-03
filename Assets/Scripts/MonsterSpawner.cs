    using System.Runtime.InteropServices;
    using System.Runtime.Versioning;
    using AI;
    using AI.Events;
    using UnityEngine;

    public class MonsterSpawner
    {
        private GameObject _monsterPrefab;
        private static Camera _cam;
        private AIEventSystem _eventSystem;
        private EntityManager _entityManager;

        private FloatVariable _monstersLeftUI;
        private FloatVariable _timerUI;
        private int _monstersLeft = 10;
        private float _cooldown = 10f;
        private float _timer = 0f;
        public void Init(GameObject prefab, AIEventSystem eventSystem, EntityManager entityManager)
        {
            _cam = Camera.main;
            _monsterPrefab = prefab;
            _eventSystem = eventSystem;
            _entityManager = entityManager;

            _monstersLeftUI = Resources.Load<FloatVariable>("UI/MonstersLeft");
            _timerUI = Resources.Load<FloatVariable>("UI/MonsterCooldown");
            if (_monstersLeftUI == null || _timerUI == null)
            {
                Debug.LogError("No asset file found");
            }
        }
        
        public void Tick()
        {
            _timer += Time.deltaTime;
            
            _timerUI.Value = _timer;
            _monstersLeftUI.Value = _monstersLeft;

            if (_timer < _cooldown)
            {
                return;
            }

            if (_monstersLeft <= 0)
            {
                return;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                Spawn();
                _timer = 0;
                _monstersLeft--;
            }
        }

        private void Spawn()
        {
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) {
                var monster = GameObject.Instantiate(_monsterPrefab, hit.point, Quaternion.identity);
                var ai = monster.GetComponent<AIBehavior>();
                ai.Init(_eventSystem);
                _entityManager.AddEntity(ai);
            }
        }
    }