using System;
using UnityEngine;

    [Serializable]
    public class Stats
    {
        public int health => _health;
        public int maxHealth => _maxHealth;
        public int stamina => _stamina;
        public int mana =>  _mana;
        public int morale => _morale;
        public bool hasWeapon => _hasWeapon;
        public float attackRange => _attackRange;

        [SerializeField]  private int _health;
        [SerializeField]  private int _maxHealth;
        [SerializeField]  private int _stamina;
        [SerializeField]  private int _maxStamina;
        [SerializeField]  private int _mana;
        [SerializeField]  private int _maxMana;
        [SerializeField]  private int _morale;
        [SerializeField]  private int _maxMorale;
        [SerializeField]  private bool _hasWeapon;
        [SerializeField] private float _attackRange;
        
        private float recoverySpeed = 1;

        public Stats()
        {
            _maxHealth = 100;
            _maxStamina = 100;
            _maxMana = 100;
            _maxMorale = 100;
            _health = _maxHealth;
            _stamina = _maxStamina;
            _mana = _maxMana;
            _morale = _maxMorale;

            _hasWeapon = true;
            _attackRange = 30;
        }
        
        public void Init()
        {
        }
        
        public void Tick()
        {
            _stamina += (int)(Time.deltaTime * recoverySpeed);
            _morale += (int)(Time.deltaTime * recoverySpeed);
            _mana += (int)(Time.deltaTime * recoverySpeed);

            _stamina = Mathf.Min(_stamina, _maxStamina);
            _morale = Mathf.Min(_morale, _maxMorale);
            _mana = Mathf.Min(_maxMana, _maxMorale);
        }

        public void ReduceHealth(int value)
        {
            _health -= value;
        }

        public void ReduceMana(int value)
        {
            _mana -= value;
        }

        public void ReduceMorale(int value)
        {
            _morale -= value;
        }
    }