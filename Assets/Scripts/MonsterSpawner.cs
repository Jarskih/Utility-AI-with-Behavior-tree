    using System.Runtime.InteropServices;
    using AI;
    using AI.Events;
    using UnityEngine;

    public class MonsterSpawner
    {
        private GameObject _monsterPrefab;
        private static Camera _cam;
        private static readonly RaycastHit[] _results = new RaycastHit[10];
        private AIEventSystem _eventSystem;
        private EntityManager _entityManager;

        public MonsterSpawner()
        {
        }
        public void Init(GameObject prefab, AIEventSystem eventSystem, EntityManager entityManager)
        {
            _cam = Camera.main;
            _monsterPrefab = prefab;
            _eventSystem = eventSystem;
            _entityManager = entityManager;
        }
        
        public void Tick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Spawn();  
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