using System.Collections.Generic;
using AI;
using UnityEngine;

public class WinCondition
{
    private List<AIBehavior> _heroes;
    private GameObject _loseUI;
    private GameObject _wonUI;
    private Waypoints _waypoints;
    private MonsterSpawner _monsterSpawner;
    
    public void Init(EntityManager _entityManager, MonsterSpawner monsterSpawner)
    {
        _monsterSpawner = monsterSpawner;
        _waypoints = GameObject.FindObjectOfType<Waypoints>();
        _heroes = _entityManager.GetFriendlyEntities(0);
        _loseUI = GameObject.FindObjectOfType<LoseUI>().gameObject;
        _wonUI = GameObject.FindObjectOfType<WinUI>().gameObject;
        
        _loseUI.SetActive(false);
        _wonUI.SetActive(false);
    }

    public void Tick()
    {
        if (_monsterSpawner.GetMonstersLeft() <= 0)
        {
            GameLost();
            return;
        }
        
        foreach (var hero in _heroes)
        {
            if (Vector3.Distance(_waypoints.GetLastWaypoint().transform.position, hero.transform.position) < 5)
            {
                GameLost();
                return;
            }
        }
        
        foreach (var hero in _heroes)
        {
            if (hero.IsActive()) return;
        }
        
        GameWon();
    }

    private void GameLost()
    { 
        _loseUI.SetActive(true);
    }

    private void GameWon()
    {
        _wonUI.SetActive(true);
    }
}
